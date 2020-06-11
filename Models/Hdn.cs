using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Hdn
    {
        public int MaHdn { get; set; }
        public int? MaNcc { get; set; }
        public int? MaNv { get; set; }
        public DateTime NgayNhap { get; set; }
        public bool TrangThaiDaXoa { get; set; }
        public string MoTa { get; set; }

        public virtual Ncc Ncc { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual ICollection<CtHdn> CtHdn { get; set; }
    }
}
