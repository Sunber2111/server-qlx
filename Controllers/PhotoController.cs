using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Security.ServerName;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly IServerName _serverName;

        public PhotoController(IWebHostEnvironment environment, IServerName serverName)
        {
            _environment = environment;
            _serverName = serverName;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var image = System.IO.File.OpenRead("Uploads/" + id);
                return File(image, "image/jpeg");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        public async Task<string> Post([FromForm]IFormFile files)
        {
            try
            {
                if (files.Length > 0)
                {
                    var id = Guid.NewGuid().ToString();
                    using (FileStream filestream = System.IO.File.Create("Uploads/" + id + files.FileName))
                    {
                        await files.CopyToAsync(filestream);
                        filestream.Flush();
                        return _serverName.GetServerName() + "/api/Photo/" + id + files.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

    }
}