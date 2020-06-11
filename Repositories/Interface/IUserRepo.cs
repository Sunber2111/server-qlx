using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IUserRepo : IRepoBase<NhanVien, NhanVienDTO>
    {
        Task<UserCurrent> Login(TaiKhoan tk);

        Task<bool> UpdateStateActive(int nv);

        Task<bool> UpdateBelongAccount(string username, int idNv);

        Task<bool> ResetPassword(string email);

        Task<bool> ResetPassword(int maNv);

        Task<bool> RegisAccount(NhanVien nv);

        Task<UserCurrent> GetCurrentUser();
    }
}