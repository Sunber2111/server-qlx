using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Kho
    {
        public int MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
        public bool NgungHoatDong { get; set; }

        public virtual ICollection<CtHdn> CtHdn { get; set; }
        public virtual ICollection<CtHdx> CtHdx { get; set; }
        public virtual ICollection<CtKho> CtKho { get; set; }
    }
}
