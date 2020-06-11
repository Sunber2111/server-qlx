using System.Collections.Generic;
using API.Models;

namespace API.ViewModels
{
    public class QueryAddHdx
    {
        public KhachHang KhachHang { get; set; }

        public List<CtDTO> CtHdx { get; set; }
    }

    public class CtDTO
    {
        public int MaCtKho { get; set; }

        public int GiaXuat { get; set; }
    }
}