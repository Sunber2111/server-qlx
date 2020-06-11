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
    public class LoaiXeController:ControllerBase
    {
        private readonly ILoaiXeRepo repo;
        public LoaiXeController(ILoaiXeRepo lxRepo)
        {
            repo = lxRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoaiXeDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllLoaiXe();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<LoaiXeDTO>> Add(LoaiXe lx)
        {
            try
            {
                return await repo.AddLoaiXe(lx);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(LoaiXe lx)
        {
            try
            {
                if (await repo.Update(lx))
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
                return await repo.DeleteLoaiXe(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}