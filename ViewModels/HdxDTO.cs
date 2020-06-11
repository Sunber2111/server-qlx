using System;
using System.Collections.Generic;

namespace API.ViewModels
{
    public class HdxDTO
    {
        public int MaHdx { get; set; }
        public string TenKh { get; set; }
        public string TenNv { get; set; }
        public DateTime NgayXuat { get; set; }


        public ICollection<CtHdxDTO> CtHdx { get; set; }
    }
}