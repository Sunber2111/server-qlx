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
    public class BaoHanhController : ControllerBase
    {
        private readonly IBaoHanhRepo repo;
        public BaoHanhController(IBaoHanhRepo bhRepo)
        {
            repo = bhRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<BaoHanhDTO>>> GetAll() 
        {
            try
            {
                return await repo.GetAllBaoHanh();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaoHanhDTO>> Add(BaoHanh bh) 
        {
            try
            {
                return await repo.AddBH(bh);
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
                return await repo.DeleteBH(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}