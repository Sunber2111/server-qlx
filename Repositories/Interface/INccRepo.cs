using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;

namespace API.Repository.Interface
{
    public interface INccRepo : IRepoBase<Ncc, NccDTO>
    {
        Task<List<NccDTO>> GetNcc();

        Task<NccDTO> AddNcc(Ncc ncc);
        
        Task<bool> DeleteNcc(int id);
    }
}