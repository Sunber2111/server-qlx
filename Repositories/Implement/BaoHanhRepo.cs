using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Implement
{
    public class BaoHanhRepo : RepoBase<BaoHanh, BaoHanhDTO>, IBaoHanhRepo
    {
        public BaoHanhRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<BaoHanhDTO> AddBH(BaoHanh bh)
        {
           bh.TrangThaiDaXoa  =false;
           var xe = await db.Xe.SingleOrDefaultAsync(x=>x.MaXe == bh.MaXe);
           if(xe.DaNgungBan == false)
                throw new Exception("Xe Chưa Được Bán - Không Thêm Được Phiếu Bảo Hành");
            
           return await Insert(bh);
        }

        public async Task<bool> DeleteBH(int id)
        {
            var bh = await db.BaoHanh.SingleOrDefaultAsync(b=>b.MaBh == id);
            if(bh == null)
                throw new Exception("Phiếu bảo Hành không tồn tại");
            if(bh.NhanVien != null || bh.KhachHang != null || bh.Xe !=null)
                bh.TrangThaiDaXoa = true;
            return await db.SaveChangesAsync() >0;
        }

        public async Task<List<BaoHanhDTO>> GetAllBaoHanh()
        {
            var ds = await db.BaoHanh.Where(x=>x.TrangThaiDaXoa == false).ToListAsync();
            return mapper.Map<List<BaoHanh>,List<BaoHanhDTO>>(ds);
        }
    }
}