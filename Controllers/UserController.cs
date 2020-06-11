using System;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo repo;
        public UserController(IUserRepo userRepo)
        {
            repo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserCurrent>> Login(TaiKhoan tk)
        {
            try
            {
                return await repo.Login(tk);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("active/{id}")]
        public async Task<ActionResult<bool>> UpdateStateActive(int id)
        {
            try
            {
                return await repo.UpdateStateActive(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("changebelong/{id}")]
        public async Task<ActionResult<bool>> ChangeBelong(string username, int id)
        {
            try
            {
                return await repo.UpdateBelongAccount(username, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("sendmess/{id}")]
        public async Task<ActionResult<bool>> ResetPass(int id)
        {
            try
            {
                return await repo.ResetPassword(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet("sendmess/email/{username}")]
        public async Task<ActionResult<string>> ResetPassUser(string username)
        {
            try
            {
                await repo.ResetPassword(username);
                return Ok("Đã Gửi Qua Email - Vui Lòng Check Mail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("current")]
        public async Task<ActionResult<UserCurrent>> GetCurrent()
        {
            try
            {
                return await repo.GetCurrentUser();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("regis")]
        public async Task<ActionResult<bool>> Regis(NhanVien nv)
        {
            try
            {
                return await repo.RegisAccount(nv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}