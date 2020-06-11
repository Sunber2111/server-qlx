using System;

namespace API.Models
{
    public class BaoHanh
    {
        public int MaBh { get; set; }

        public int MaNv { get; set; }

        public int MaKh { get; set; }

        public int MaXe { get; set; }

        public int ThoiGian { get; set; }

        public DateTime NgayMua { get; set; }

        public int GiaBan { get; set; }

        public bool TrangThaiDaXoa { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual Xe Xe { get; set; }
    }
}
