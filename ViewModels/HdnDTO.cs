using System;
using System.Collections.Generic;

namespace API.ViewModels
{
    public class HdnDTO
    {
        public int MaHdn { get; set; }
        public string TenNcc { get; set; }
        public string TenNv { get; set; }
        public DateTime? NgayNhap { get; set; }

        public ICollection<CtHdnDTO> CtHdn { get; set; }
    }
}