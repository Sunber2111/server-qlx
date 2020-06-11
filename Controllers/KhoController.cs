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
    public class KhoController : ControllerBase
    {
        private readonly IKhoRepo repo;
        public KhoController(IKhoRepo khoRepo)
        {
            repo = khoRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<KhoDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllKho();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<KhoDTO>> Add(Kho kho)
        {
            try
            {
                return await repo.AddKho(kho);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Kho kho)
        {
            try
            {
                if (await repo.Update(kho))
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
               return await repo.DeleteKho(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}