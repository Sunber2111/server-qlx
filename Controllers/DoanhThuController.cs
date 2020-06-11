using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Repositories.Interface;
using API.ViewModels.Chart;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DoanhThuController : ControllerBase
    {
        protected readonly IDoanhThu repo;
        public DoanhThuController(IDoanhThu repoDT)
        {
            repo = repoDT;
        }

        [HttpGet]
        public async Task<ActionResult<List<DoanhThu>>> GetAll()
        {
            try
            {
                return await repo.getAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("top5")]
        public async Task<ActionResult<List<DoanhThu>>> GetTop5()
        {
            try
            {
                return await repo.GetTop5ByCategory();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fastnews")]
        public async Task<ActionResult<FastNews>> GetNews()
        {
            try
            {
                return await repo.GetFastNews();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("top3")]
        public async Task<ActionResult<List<DoanhThu>>> GetTop3()
        {
            try
            {
                return await repo.GetTop3();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}