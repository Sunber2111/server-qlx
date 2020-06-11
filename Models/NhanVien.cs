using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class NhanVien
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

        public string Email { get; set; }

        public string MatKhau { get; set; }

        public bool KichHoat { get; set; }

        public DateTime NgaySinh { get; set; }

        public bool DaNghiViec { get; set; }

        public virtual ICollection<BaoHanh> BaoHanh { get; set; }

        public virtual ICollection<Hdn> Hdn { get; set; }

         public virtual ICollection<Hdx> Hdx { get; set; }
    }
}
