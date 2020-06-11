using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface INhanVienRepo : IRepoBase<NhanVien, NhanVienDTO>
    {
        Task<bool> DeleteNhanVien(int id);

        Task<NhanVienDTO> AddNhanVien(NhanVien nv);
        
        Task<bool> UpdateNhanVien(NhanVien nv);

        Task<List<NhanVienDTO>> GetAllNv();

    }
}
