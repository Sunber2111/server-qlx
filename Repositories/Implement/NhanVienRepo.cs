using API.Models;
using API.Repository.Interface;
using API.Security.Mail;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Implement
{
    public class NhanVienRepo : RepoBase<NhanVien, NhanVienDTO>, INhanVienRepo
    {
        public NhanVienRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<NhanVienDTO> AddNhanVien(NhanVien nv)
        {
            nv.DaNghiViec = false;

            var isEmail = await db.NhanVien.FirstOrDefaultAsync(x => x.Email == nv.Email);
            if (isEmail != null)
                throw new Exception("Email Đã Tồn Tại");

            return await Insert(nv);
        }

        public async Task<bool> DeleteNhanVien(int id)
        {
            var nv = await db.NhanVien.SingleOrDefaultAsync(t => t.MaNv == id);
            if (nv == null)
                throw new Exception("Mã Nhân Viên Không Tồn Tại");
            if (nv.Hdx.Count > 0 ||
                nv.Hdn.Count > 0 ||
                nv.BaoHanh.Count > 0)
            {
                nv.DaNghiViec = true;
            }
            else
            {
                db.NhanVien.Remove(nv);
            }
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<NhanVienDTO>> GetAllNv()
        {
            var ds = await db.NhanVien.Where(x => x.DaNghiViec == false).ToListAsync();
            return mapper.Map<List<NhanVien>, List<NhanVienDTO>>(ds);
        }

        public async Task<bool> UpdateNhanVien(NhanVien nv)
        {
            if (await db.NhanVien.SingleOrDefaultAsync(n => n.TenDangNhap == nv.TenDangNhap
                                                       && n.MaNv != nv.MaNv && n.TenDangNhap != null) != null)
                throw new Exception("Tên Đăng Nhập Đã Tồn Tại");
            return await Update(nv);
        }
    }
}
