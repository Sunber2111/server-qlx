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
    public class HdxController : ControllerBase
    {
        private readonly IHdxRepo repo;
        public HdxController(IHdxRepo hdxRepo)
        {
            repo = hdxRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<HdxDTO>>> GetAll() 
        {
            try
            {
                return await repo.GetAllHdx();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<HdxDTO>> Add(QueryAddHdx query) 
        {
            try
            {
                return await repo.AddHdx(query);
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
                return await repo.DeleteHdx(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}