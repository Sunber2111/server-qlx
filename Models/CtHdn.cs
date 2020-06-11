using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class CtHdn
    {
        public int Id { get; set; }
        public int MaHdn { get; set; }
        public int MaXe { get; set; }
        public int MaKho { get; set; }
        public double GiaNhap { get; set; }

        public virtual Hdn Hdn { get; set; }
        public virtual Kho Kho { get; set; }
        public virtual Xe Xe { get; set; }
    }
}
