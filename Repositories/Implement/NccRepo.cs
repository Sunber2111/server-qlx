using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;

namespace API.Repositories.Implement
{

    public class NccRepo : RepoBase<Ncc, NccDTO>, INccRepo
    {
        public NccRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<NccDTO> AddNcc(Ncc ncc)
        {
            ncc.NgungCungCap = false;
            return await Insert(ncc);
        }

        public async Task<bool> DeleteNcc(int id)
        {
            var ncc = await db.Ncc.SingleOrDefaultAsync(ncc => ncc.MaNcc == id);
            if (ncc == null)
                throw new Exception("Không Tồn Tại Nhà Cung Cấp");
            if (ncc.Xe.Count > 0 ||
                ncc.Hdn.Count > 0)
            {
                ncc.NgungCungCap = true;
            }
            else
            {
                db.Ncc.Remove(ncc);
            }
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<NccDTO>> GetNcc()
        {
            var ds = await db.Ncc.Where(ncc => ncc.NgungCungCap == false).ToListAsync();
            return mapper.Map<List<Ncc>, List<NccDTO>>(ds);
        }
    }
}
