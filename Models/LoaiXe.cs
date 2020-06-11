using System;
using System.Collections.Generic;

namespace API.Models
{
    public class LoaiXe
    {
        public int MaLoaiXe { get; set; }
        public string TenLoaiXe { get; set; }
        public bool DaNgungNhap { get; set; }

        public virtual ICollection<Xe> Xe { get; set; }
    }
}
