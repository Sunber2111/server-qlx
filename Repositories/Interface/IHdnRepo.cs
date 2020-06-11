using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IHdnRepo : IRepoBase<Hdn, HdnDTO>
    {
        Task<HdnDTO> AddHdn(Hdn ct);

        Task<bool> DeleteHdn(int id);

        Task<List<HdnDTO>> GetAllHdn();

        Task<bool> AddListHdn(List<Xe> DsXe, List<Hdn> DsHdn);
    }
}