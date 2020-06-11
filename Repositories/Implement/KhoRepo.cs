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
    public class KhoRepo : RepoBase<Kho, KhoDTO>, IKhoRepo
    {
        public KhoRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<KhoDTO> AddKho(Kho kho)
        {
            var isNameExists = await db.Kho.FirstOrDefaultAsync(kh => kh.TenKho == kho.TenKho);
            if (isNameExists != null)
                throw new Exception("Tên Kho Đã Tồn Tại");
            kho.NgungHoatDong = false;
            await db.Kho.AddAsync(kho);
            await db.SaveChangesAsync();
            return mapper.Map<Kho, KhoDTO>(kho);
        }

        public async Task<bool> DeleteKho(int id)
        {
            var kho = await db.Kho.SingleOrDefaultAsync(t=>t.MaKho == id);
            if(kho == null)
                throw new Exception("Kho Không Tồn Tại");
            if(kho.CtKho.Count >0)
                throw new Exception("Kho Còn Hàng, Không Thể Xóa");
            if(kho.CtHdn.Count >0 || kho.CtHdx.Count >0 )
                kho.NgungHoatDong = true;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<KhoDTO>> GetAllKho()
        {
            var ds = await db.Kho.Where(Kho=>Kho.NgungHoatDong == false).ToListAsync();
            return mapper.Map<List<Kho>,List<KhoDTO>>(ds);
        }
    }
}