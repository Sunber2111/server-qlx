using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IBaoHanhRepo: IRepoBase<BaoHanh,BaoHanhDTO>
    {
        Task<BaoHanhDTO> AddBH(BaoHanh bh);

        Task<bool> DeleteBH(int id);

        Task<List<BaoHanhDTO>> GetAllBaoHanh();
    }
}