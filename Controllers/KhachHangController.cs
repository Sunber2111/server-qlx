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
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangRepo repo;
        public KhachHangController(IKhachHangRepo khRepo)
        {
            repo = khRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<KhachHangDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllKhachHang();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<KhachHangDTO>> Add(KhachHang kh)
        {
            try
            {
                return await repo.AddKhachHang(kh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(KhachHang kh)
        {
            try
            {
                if (await repo.Update(kh))
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
               return await repo.DeleteKH(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sdt/{phone}")]
        public async Task<ActionResult<KhachHangDTO>> FindByPhone(string phone)
        {
            try
            {
               return await repo.FindByPhone(phone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cmnd/{cmnd}")]
        public async Task<ActionResult<KhachHangDTO>> FindByCMND(string cmnd)
        {
            try
            {
               return await repo.FindByCMND(cmnd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}