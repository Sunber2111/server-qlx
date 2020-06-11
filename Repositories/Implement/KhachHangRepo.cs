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
    public class KhachHangRepo : RepoBase<KhachHang, KhachHangDTO>, IKhachHangRepo
    {
        public KhachHangRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<KhachHangDTO> AddKhachHang(KhachHang kh)
        {
            kh.LienHe = true;
            return await Insert(kh);
        }

        public async Task<bool> DeleteKH(int id)
        {
            var kh = await db.KhachHang.SingleOrDefaultAsync(x => x.MaKh == id);
            if (kh == null)
                throw new Exception("Không Tồn Tại Khách Hàng Cần Xóa");
            if (kh.Hdx.Count > 0 || kh.BaoHanh.Count > 0)
            {
                kh.LienHe = false;
                await db.SaveChangesAsync();
                return true;
            }
            db.KhachHang.Remove(kh);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<KhachHangDTO> FindByCMND(string cmnd)
        {
            var data = await db.KhachHang.FirstOrDefaultAsync(x => x.Cmnd == cmnd);
            return mapper.Map<KhachHang, KhachHangDTO>(data);
        }

        public async Task<KhachHangDTO> FindByPhone(string number)
        {
            var data = await db.KhachHang.FirstOrDefaultAsync(x => x.Sdt == number);
            return mapper.Map<KhachHang, KhachHangDTO>(data);
        }

        public async Task<List<KhachHangDTO>> GetAllKhachHang()
        {
            var ds = await db.KhachHang.Where(kh => kh.LienHe == true).ToListAsync();
            return mapper.Map<List<KhachHang>, List<KhachHangDTO>>(ds);
        }
    }
}