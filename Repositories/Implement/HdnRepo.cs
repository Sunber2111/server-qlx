using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Models;
using API.Repository.Interface;
using API.Security.UserAccessor;
using API.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Implement
{
    public class HdnRepo : RepoBase<Hdn, HdnDTO>, IHdnRepo
    {
        protected readonly IUserAccessor userAccessor;
        public HdnRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            userAccessor = serviceProvider.GetRequiredService<IUserAccessor>();
        }

        public async Task<HdnDTO> AddHdn(Hdn ct)
        {

            if (userAccessor.GetCurrentUserId() == null)
                throw new Exception("Chưa có nhân viên lập hóa đơn");

            var maNv = Convert.ToInt32(userAccessor.GetCurrentUserId());

            var nv = await db.NhanVien.SingleOrDefaultAsync(nv => nv.MaNv == maNv);

            if (nv == null)
                throw new Exception("Người lập hóa đơn không phải là nhân viên");

            ct.MaNv = maNv;
            ct.TrangThaiDaXoa = false;
            db.Hdn.Add(ct);
            await db.SaveChangesAsync();


            foreach (var x in ct.CtHdn)
            {
                var store = await db.Kho.SingleOrDefaultAsync(t => t.MaKho == x.MaKho);
                var ctKho = store.CtKho.FirstOrDefault(t => t.MaXe == x.MaXe);
                if (ctKho != null)
                {
                    // ctKho.Soluong += x.SoLuong;
                }
                else
                {
                    var ctK = new CtKho
                    {
                        MaKho = x.MaKho,
                        // Soluong = x.SoLuong,
                        MaXe = x.MaXe
                    };
                    db.CtKho.Add(ctK);
                }
                await db.SaveChangesAsync();
            }

            return mapper.Map<Hdn, HdnDTO>(ct);
        }

        public async Task<bool> DeleteHdn(int id)
        {
            var hd = await db.Hdn.SingleOrDefaultAsync(h => h.MaHdn == id);
            if (hd == null)
                throw new Exception("Hóa Đơn Không Tồn Tại");
            hd.TrangThaiDaXoa = true;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<HdnDTO>> GetAllHdn()
        {
            var ds = await db.Hdn.Where(hd => hd.TrangThaiDaXoa == false).ToListAsync();
            return mapper.Map<List<Hdn>, List<HdnDTO>>(ds);
        }


        public async Task<bool> AddListHdn(List<Xe> DsXe, List<Hdn> DsHdn)
        {
            

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach(var hdn in DsHdn)
                    {
                        if(await db.Ncc.SingleOrDefaultAsync(t=>t.MaNcc==hdn.MaNcc) == null)
                             throw new Exception($"Nhà Cung Cấp Với Mã {hdn.MaNcc} Không Tồn Tại");
                    }

                    Dictionary<int, int> dsKeyXe = new Dictionary<int, int>();
                    var oldKeyXe = 0;
                    foreach (var xe in DsXe)
                    {
                        if(await db.LoaiXe.SingleOrDefaultAsync(lx=>lx.MaLoaiXe==xe.MaLoaiXe) == null)
                            throw new Exception($"Mã Xe {xe.MaLoaiXe} Không Tồn Tại");
                        oldKeyXe=xe.MaXe;
                        xe.DaNgungBan = false;
                        xe.MaXe = 0;
                        await db.Xe.AddAsync(xe);
                        await db.SaveChangesAsync();
                        dsKeyXe.Add(oldKeyXe,xe.MaXe);
                    }

                    var value = 0;

                    foreach (var hdn in DsHdn)
                    {   
                        value = 0;
                        hdn.NgayNhap = DateTime.Now;
                        hdn.TrangThaiDaXoa = false;
                        
                        foreach (var ct in hdn.CtHdn)
                        {
                            if(!dsKeyXe.TryGetValue(ct.MaXe,out value))  
                                throw new Exception($"Mã Xe {ct.MaXe} không tồn tại trong danh sách xe thêm");
                            ct.MaXe = value;
                        }

                        await db.Hdn.AddAsync(hdn);
                        await db.SaveChangesAsync();

                        foreach (var ct in hdn.CtHdn)
                        {
                            var ctKho = new CtKho
                            {
                                MaKho = ct.MaKho,
                                MaXe = ct.MaXe
                            };
                            await db.CtKho.AddAsync(ctKho);
                            await db.SaveChangesAsync();
                        }

                    }

                    await transaction.CommitAsync() ;
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

    }
}