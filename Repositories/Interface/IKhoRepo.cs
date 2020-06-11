using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface IKhoRepo : IRepoBase<Kho, KhoDTO>
    {
        Task<KhoDTO> AddKho(Kho kho);

        Task<bool> DeleteKho(int id);
        
        Task<List<KhoDTO>> GetAllKho();
    }
}