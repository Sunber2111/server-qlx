using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Xe
    {
        public int MaXe { get; set; }

        public string TenXe { get; set; }

        public int? MaLoaiXe { get; set; }

        public int? MaNcc { get; set; }

        public bool DaNgungBan { get; set; }

        public string SoKhung { get; set; }

        public string SoMay { get; set; }

        public virtual LoaiXe LoaiXe { get; set; }

        public virtual Ncc Ncc { get; set; }

        public virtual BaoHanh BaoHanh { get; set; }

        public virtual CtHdn CtHdn { get; set; }

        public virtual CtHdx CtHdx { get; set; }

        public virtual CtKho CtKho { get; set; }
    }
}
