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
    public class HdnController : ControllerBase
    {
        private readonly IHdnRepo repo;
        public HdnController(IHdnRepo hdnRepo)
        {
            repo = hdnRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<HdnDTO>>> GetAll()
        {
            try
            {
                return await repo.GetAllHdn();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<HdnDTO>> Add(Hdn hd)
        {
            try
            {
                return await repo.AddHdn(hd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addlist")]
        public async Task<ActionResult<bool>> AddList(QueryAddList query)
        {
            try
            {
                return await repo.AddListHdn(query.dsXe, query.dsHdn);
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
                return await repo.DeleteHdn(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}