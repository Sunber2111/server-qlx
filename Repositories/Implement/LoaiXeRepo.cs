using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implement
{
    public class LoaiXeRepo : RepoBase<LoaiXe, LoaiXeDTO>, ILoaiXeRepo
    {
        public LoaiXeRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<LoaiXeDTO> AddLoaiXe(LoaiXe lx)
        {
            lx.DaNgungNhap = false;
            if(await db.LoaiXe.FirstOrDefaultAsync(x=>x.TenLoaiXe == lx.TenLoaiXe.Trim()) != null)
                throw new Exception("Xe Bị Trùng Tên");
            return await Insert(lx);
        }

        public async Task<bool> DeleteLoaiXe(int id)
        {
            var lx = await db.LoaiXe.SingleOrDefaultAsync(x => x.MaLoaiXe == id);
            if (lx == null)
                throw new Exception("Không Tồn Tại Loại Xe Này");
            if (lx.Xe.Count > 0)
                lx.DaNgungNhap = true;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<LoaiXeDTO>> GetAllLoaiXe()
        {
            var ds = await db.LoaiXe.Where(x=>x.DaNgungNhap == false).ToListAsync();
            return mapper.Map<List<LoaiXe>,List<LoaiXeDTO>>(ds);
        }
    }
}