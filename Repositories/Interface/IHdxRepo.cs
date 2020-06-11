using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IHdxRepo: IRepoBase<Hdx,HdxDTO>
    {
        Task<HdxDTO> AddHdx(QueryAddHdx query);

        Task<bool> DeleteHdx(int id);

        Task<List<HdxDTO>> GetAllHdx();
    }
}