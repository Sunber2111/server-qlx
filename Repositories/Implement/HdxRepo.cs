using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Models;
using API.Repository.Interface;
using API.Security.UserAccessor;
using API.ViewModels;
using System.Collections.Generic;

namespace API.Repositories.Implement
{
    public class HdxRepo : RepoBase<Hdx, HdxDTO>, IHdxRepo
    {
        protected readonly IUserAccessor userAccessor;
        public HdxRepo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            userAccessor = serviceProvider.GetRequiredService<IUserAccessor>();
        }

        public async Task<HdxDTO> AddHdx(QueryAddHdx query)
        {

            // if (userAccessor.GetCurrentUserId() == null)
            //     throw new Exception("Người lập hóa đơn không phải là nhân viên");

            // var maNv = Convert.ToInt32(userAccessor.GetCurrentUserId());

            // var nv = await db.NhanVien.SingleOrDefaultAsync(nv => nv.MaNv == maNv);

            // if (nv == null)
            //     throw new Exception("Người lập hóa đơn không phải là nhân viên");

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    query.KhachHang.LienHe = true;
                    if (query.KhachHang.MaKh == 0)
                    {
                        await db.KhachHang.AddAsync(query.KhachHang);
                        await db.SaveChangesAsync();
                    }

                    var hdx = new Hdx();
                    hdx.MaKh = query.KhachHang.MaKh;
                    hdx.NgayXuat = DateTime.Now;

                    hdx.CtHdx = new List<CtHdx>();

                    foreach (var ct in query.CtHdx)
                    {
                        var ctKho = await db.CtKho.SingleOrDefaultAsync(x=>x.Id == ct.MaCtKho);
                        if(ctKho==null)
                             throw new Exception("Xe Không Tồn Tại Trong Kho");
                        
                        var p = await db.Xe.SingleOrDefaultAsync(e => e.MaXe == ctKho.MaXe);
                        if (p == null)
                            throw new Exception("Xe không tồn tại");
                        if (p.DaNgungBan == true)
                            throw new Exception("Xe Đã Bán");
                        p.DaNgungBan = true;

                        db.CtKho.Remove(ctKho);
                        await db.SaveChangesAsync();
                        hdx.CtHdx.Add(new CtHdx{
                            MaXe = ctKho.MaXe,
                            GiaXuat=ct.GiaXuat,
                            MaKho = ctKho.MaKho
                        });
                    }

                    hdx.MaNv = 1;
                    hdx.TrangThaiDaXoa = false;
                    db.Hdx.Add(hdx);
                    await db.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return mapper.Map<Hdx, HdxDTO>(hdx);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }


        }

        public async Task<bool> DeleteHdx(int id)
        {
            var hd = await db.Hdx.SingleOrDefaultAsync(h => h.MaHdx == id);
            if (hd == null)
                throw new Exception("Không Tồn Tại Hóa Đơn Này");
            hd.TrangThaiDaXoa = true;
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<List<HdxDTO>> GetAllHdx()
        {
            var ds = await db.Hdx.Where(h => h.TrangThaiDaXoa == false).ToListAsync();
            return mapper.Map<List<Hdx>, List<HdxDTO>>(ds);
        }
    }
}