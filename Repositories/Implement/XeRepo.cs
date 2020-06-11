using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implement
{
    public class XeRepo : RepoBase<Xe, XeDTO>, IXeRepo
    {
        public XeRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<XeDTO> AddXe(Xe xe)
        {
            xe.DaNgungBan = false;
            if (await db.Xe.FirstOrDefaultAsync(x => x.TenXe == xe.TenXe.Trim()) != null)
                throw new Exception("Xe Bị Trùng Tên");
            return await Insert(xe);
        }

        public async Task<bool> DeleteXe(int id)
        {
            var xe = await db.Xe.SingleOrDefaultAsync(x => x.MaXe == id);
            if (xe == null)
                throw new Exception("Xe không tồn tại");
            if (xe.CtHdn != null || xe.CtHdx != null || xe.CtKho != null)
            {
                xe.DaNgungBan = true;
            }
            else
            {
                db.Xe.Remove(xe);
            }
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<XeDTO>> GetAllByIdCategory(int idCategory)
        {
            var ds = await db.Xe.Where(xe => xe.DaNgungBan == false && xe.MaLoaiXe == idCategory).ToListAsync();
            return mapper.Map<List<Xe>, List<XeDTO>>(ds);
        }

        public async Task<List<XeDTO>> GetAllXeDTO()
        {
            var ds = await db.Xe.Where(xe => xe.DaNgungBan == false).ToListAsync();
            return mapper.Map<List<Xe>, List<XeDTO>>(ds);
        }

        public async Task<bool> UpdateXe(Xe xe)
        {
            var car = await db.Xe.SingleOrDefaultAsync(x => x.MaXe == xe.MaXe);
            if (car == null)
                throw new Exception("Không tồn tại xe cần sửa");
            if (await db.Xe.FirstOrDefaultAsync(x => x.TenXe == xe.TenXe.Trim() && x.MaXe != xe.MaXe) != null)
                throw new Exception("Xe Bị Trùng Tên");
            car.MaLoaiXe = xe.MaLoaiXe;
            car.TenXe = xe.TenXe;
            return await db.SaveChangesAsync() > 0;
        }
    }
}