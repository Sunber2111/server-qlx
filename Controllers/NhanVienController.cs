using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController:ControllerBase
    {
        private readonly INhanVienRepo repo;
        public NhanVienController(INhanVienRepo nvRepo)
        {
            repo = nvRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<NhanVienDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllNv();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVienDTO>> GetById(int id)
        {
            try
            {
                return await repo.GetById(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<NhanVienDTO>> Add(NhanVien nv)
        {
            try
            {
                return await repo.AddNhanVien(nv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(NhanVien nv)
        {
            try
            {
                if (await repo.UpdateNhanVien(nv))
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
                return await repo.DeleteNhanVien(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}