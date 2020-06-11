using System;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Mail;
using API.Models;
using API.Repository.Interface;
using API.Security.Jwt;
using API.Security.Mail;
using API.Security.UserAccessor;
using API.ViewModels;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Repositories.Implement
{
    public class UserRepo : RepoBase<NhanVien, NhanVienDTO>, IUserRepo
    {

        protected readonly IJwtgenerator jwtgenerator;

        protected readonly IEmailSender emailSender;

        protected readonly IUserAccessor userAccessor;

        public UserRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            jwtgenerator = serviceProvider.GetRequiredService<IJwtgenerator>();

            emailSender = serviceProvider.GetRequiredService<IEmailSender>();

            userAccessor = serviceProvider.GetRequiredService<IUserAccessor>();
        }

        public async Task<UserCurrent> GetCurrentUser()
        {
            var idStr = userAccessor.GetCurrentUserId();
            var id = Convert.ToInt32(idStr);
            var result = await db.NhanVien.SingleOrDefaultAsync(acc => acc.MaNv == id);
            if (result == null)
                throw new Exception("Không tồn tại nhân viên");
            if (!result.KichHoat)
                throw new Exception("Tài Khoản Bị Vô Hiệu Hóa");
            if (result.MaChucVu == null)
                throw new Exception("Nhân Viên Chưa Có Chức Vụ");

            var token = jwtgenerator.CreateToken(result);

            var userCurrent = new UserCurrent
            {
                MaNv = result.MaNv,
                TenNv = result.TenNv,
                MaChucVu = (int)result.MaChucVu,
                Token = token,
                Hinh=result.Hinh
            };

            return userCurrent;
        }

        public async Task<UserCurrent> Login(TaiKhoan tk)
        {
            var acc = await db.NhanVien
                        .FirstOrDefaultAsync(nv => nv.TenDangNhap == tk.TenDangNhap && nv.MatKhau == tk.MatKhau);
            if (acc == null)
                throw new Exception("Sai Tài Khoản Hoặc Mật Khẩu");

            if (acc.KichHoat == false)
                throw new Exception("Tài Khoản Đã Bị Vô Hiệu Hóa");

            if (acc.MaChucVu == null)
                throw new Exception("Tài Khoản Chưa Được Phân Quyền");

            var user = new UserCurrent
            {
                MaNv = acc.MaNv,
                TenNv = acc.TenNv,
                Token = jwtgenerator.CreateToken(acc),
                MaChucVu = (int)acc.MaChucVu,
            };

            return user;
        }

        public async Task<bool> RegisAccount(NhanVien nv)
        {
            if (nv.TenDangNhap == null)
                throw new Exception("Tên đăng nhập không được để trống");

            if (nv.TenDangNhap.Trim().Length <= 6)
                throw new Exception("Tên đăng nhập phải nhiều hơn hoặc bằng 7 kí tự trả lên");

            var check = await db.NhanVien.FirstOrDefaultAsync(n => n.TenDangNhap == nv.TenDangNhap);

            if (check != null)
                throw new Exception("Tên đăng nhập Đã Bị Trùng");

            var emp = await db.NhanVien.SingleOrDefaultAsync(n => n.MaNv == nv.MaNv);

            if (emp == null)
                throw new Exception("Không Tồn Tại Nhân Viên");

            if (emp.Email == null)
                throw new Exception("Chưa đăng kí gmail nên không thể tạo tài khoản");

            emp.TenDangNhap = nv.TenDangNhap;

            var newPassword = Guid.NewGuid().ToString().Substring(0, 8);

            emp.MatKhau = newPassword;
            emp.KichHoat = true;

            await db.SaveChangesAsync();

            await emailSender.SendEmailAsync(new Message(new string[] { emp.Email },
                "Thay Đổi Mật Khẩu Tài Khoản (AnhHoaStore)",
                $"Mật khẩu tài khoản AnhHoaStore của bạn đã được đổi thành:  {newPassword}"));

            return true;
        }

        public async Task<bool> ResetPassword(string email)
        {
            if (email.Length <= 0 || email == "")
                throw new Exception("Email không được để trống");
            var nv = await db.NhanVien.FirstOrDefaultAsync(t => t.Email == email);
            if (nv == null)
                throw new Exception("Email của nhân viên không tồn tại");
            var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            await emailSender.SendEmailAsync(new Message(new string[] { nv.Email },
                "Thay Đổi Mật Khẩu Tài Khoản (AnhHoaStore)",
                $"Mật khẩu tài khoản AnhHoaStore của bạn đã được đổi thành:  {newPassword}"));
            nv.MatKhau = newPassword;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPassword(int maNv)
        {
            var nv = await db.NhanVien.SingleOrDefaultAsync(t => t.MaNv == maNv);
            if (nv == null)
                throw new Exception("Nhân viên không tồn tại");
            if (nv.Email == null)
                throw new Exception("Email của nhân viên không tồn tại");
            var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            await emailSender.SendEmailAsync(new Message(new string[] { nv.Email },
                "Thay Đổi Mật Khẩu Tài Khoản (AnhHoaStore)",
                $"Mật khẩu tài khoản AnhHoaStore của bạn đã được đổi thành:  {newPassword}"));
            nv.MatKhau = newPassword;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBelongAccount(string username, int idNv)
        {
            var nv = await db.NhanVien.SingleOrDefaultAsync(t => t.MaNv == idNv);
            if (nv == null)
                throw new Exception("Không Tồn Tại Nhân Viên");
            var acc = await db.NhanVien.FirstOrDefaultAsync(c => c.TenDangNhap == username && username != null);

            nv.TenDangNhap = acc.TenDangNhap;
            nv.MatKhau = acc.MatKhau;
            acc.MatKhau = null;
            acc.TenDangNhap = null;

            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStateActive(int nv)
        {
            var nhanvien = await db.NhanVien.SingleOrDefaultAsync(t => t.MaNv == nv);
            if (nhanvien == null)
                throw new Exception("Nhân Viên Không Tồn Tại");
            nhanvien.KichHoat = !nhanvien.KichHoat;
            return await db.SaveChangesAsync() > 0;
        }
    }
}