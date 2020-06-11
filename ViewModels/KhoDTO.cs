
using System.Collections.Generic;

namespace API.ViewModels
{
    public class KhoDTO
    {
       public int MaKho { get; set; }

        public string TenKho { get; set; }

        public string DiaChi { get; set; }
        
        public ICollection<CtKhoDTO> CtKho {get;set;}
    }
}