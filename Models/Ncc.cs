using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Ncc
    {
        public int MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public bool NgungCungCap { get; set; }

        public virtual ICollection<Hdn> Hdn { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}
