using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IXeRepo : IRepoBase<Xe, XeDTO>
    {
        Task<bool> DeleteXe(int id);

        Task<List<XeDTO>> GetAllXeDTO();

        Task<List<XeDTO>> GetAllByIdCategory(int idCategory);

        Task<XeDTO> AddXe(Xe xe);

        Task<bool> UpdateXe(Xe xe);
    }
}