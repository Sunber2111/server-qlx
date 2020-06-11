using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface ILoaiXeRepo : IRepoBase<LoaiXe, LoaiXeDTO>
    {
        Task<bool> DeleteLoaiXe(int id);

        Task<LoaiXeDTO> AddLoaiXe(LoaiXe lx);

        Task<List<LoaiXeDTO>> GetAllLoaiXe();
    }
}