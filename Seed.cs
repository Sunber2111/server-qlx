using System;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Seed
    {
        public static async Task SendData(DataContext context)
        {

            var nv = new NhanVien
            {
                Cmnd = "125443692",
                DaNghiViec = false,
                DiaChi = "13/5 Nguyễn Văn Cừ, Gò Vấp, TPHCM",
                GioiTinh = true,
                TenNv = "Đặng Hoàng Lan",
                KichHoat = true,
                MaChucVu = 4,
                NgaySinh = new DateTime(1999, 5, 23),
                Sdt = "09756689441",
                TenDangNhap = "revan333",
                MatKhau = "123456",
                Email="revan198545@gmail.com",
            };
            await context.NhanVien.AddAsync(nv);
            await context.SaveChangesAsync();
        }

    }
}