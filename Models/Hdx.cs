using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Hdx
    {
        public int MaHdx { get; set; }
        public int MaNv { get; set; }
        public int MaKh { get; set; }
        public DateTime NgayXuat { get; set; }
        public bool TrangThaiDaXoa { get; set; }
        public string MoTa { get; set; }

        public virtual NhanVien NhanVien { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<CtHdx> CtHdx { get; set; }
    }
}
