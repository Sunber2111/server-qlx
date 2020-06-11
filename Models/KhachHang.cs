using System;
using System.Collections.Generic;

namespace API.Models
{
    public class KhachHang
    {
        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; }
        public string Cmnd { get; set; }
        public string Sdt { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool LienHe { get; set; }

        public virtual ICollection<BaoHanh> BaoHanh { get; set; }
        public virtual ICollection<Hdx> Hdx { get; set; }
    }
}
