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
    public class NccController:ControllerBase
    {
        private readonly INccRepo repo;
        public NccController(INccRepo nvRepo)
        {
            repo = nvRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<NccDTO>>> GetAll()
        {
            try
            {
                return await repo.GetNcc();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<NccDTO>> Add(Ncc ncc)
        {
            try
            {
                return await repo.AddNcc(ncc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(Ncc ncc)
        {
            try
            {
                if (await repo.Update(ncc))
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
                return await repo.DeleteNcc(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}