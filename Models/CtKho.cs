using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class CtKho
    {
        public int MaXe { get; set; }
        public int MaKho { get; set; }
        public int Id { get; set; }

        public virtual Kho Kho { get; set; }
        public virtual Xe Xe { get; set; }
    }
}
