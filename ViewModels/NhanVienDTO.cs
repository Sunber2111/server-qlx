using System;

namespace API.ViewModels
{
    public class NhanVienDTO
    {
        public int MaNv { get; set; }

        public string TenNv { get; set; }

        public bool GioiTinh { get; set; }

        public string DiaChi { get; set; }

        public string Sdt { get; set; }

        public string Cmnd { get; set; }

        public int? MaChucVu { get; set; }

        public string TenDangNhap { get; set; }

        public string Hinh { get; set; }

        public string MatKhau { get; set; }

        public bool KichHoat { get; set; }

        public string Email { get; set; }

        public DateTime NgaySinh { get; set; }

    }
}
