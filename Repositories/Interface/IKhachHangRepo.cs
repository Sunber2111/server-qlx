using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IKhachHangRepo : IRepoBase<KhachHang, KhachHangDTO>
    {
        Task<bool> DeleteKH(int id);

        Task<KhachHangDTO> AddKhachHang(KhachHang kh);

        Task<List<KhachHangDTO>> GetAllKhachHang();

        Task<KhachHangDTO> FindByPhone(string number);

        Task<KhachHangDTO> FindByCMND(string cmnd);
    }
}