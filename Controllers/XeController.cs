using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XeController : ControllerBase
    {
        private readonly IXeRepo repo;
        public XeController(IXeRepo xeRepo)
        {
            repo = xeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<XeDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllXeDTO();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("category/{id}")]
        public async Task<ActionResult<List<XeDTO>>> GetByIdCategory(int id)
        {
            try
            {
                return await repo.GetAllByIdCategory(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<XeDTO>> Add(Xe xe)
        {
            try
            {
                return await repo.AddXe(xe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(Xe xe)
        {
            try
            {
                if (await repo.UpdateXe(xe))
                    return
                        Ok("Sửa Thành Công");

                return
                    BadRequest("Sửa Thất Bại");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                return await repo.DeleteXe(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}