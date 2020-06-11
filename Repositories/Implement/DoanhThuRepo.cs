using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using API.ViewModels.Chart;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implement
{
    public class DoanhThuRepo : IDoanhThu
    {
        protected readonly DataContext db;
        private readonly IMapper mapper;
        public DoanhThuRepo(DataContext Db, IMapper _mapper)
        {
            mapper = _mapper;
            db = Db;
        }
        public async Task<List<DoanhThu>> getAll()
        {
            var ds = new List<DoanhThu>();
            var dsHDX = await db.Hdx.ToListAsync();
            var day = DateTime.Now;

            var month = day.Month;
            var year = day.Year;

            for (int i = 0; i <= 12; i++)
            {
                if (month + i <= 12)
                {
                    var dsSup = dsHDX.Where(x => x.NgayXuat.Month == (month + i)
                                                && x.NgayXuat.Year == (year - 1)).ToList();
                    ds.Add(new DoanhThu
                    {
                        Name = $"{month + i}/{year - 1}",
                        Value = dsSup.Count
                    });
                }
                else
                {
                    var dsSup = dsHDX.Where(x => x.NgayXuat.Month == ((month + i) - 12)
                                                && x.NgayXuat.Year == year).ToList();
                    ds.Add(new DoanhThu
                    {
                        Name = $"{month + i - 12}/{year}",
                        Value = dsSup.Count
                    });
                }
            }
            return ds;
        }

        public async Task<FastNews> GetFastNews()
        {
            var fn = new FastNews();

            fn.TongKh = db.KhachHang.Count();
            fn.TongNv = db.NhanVien.Where(x => x.DaNghiViec == false).Count();
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            fn.TongHdxHomNay = db.Hdx.AsEnumerable().Where(x => x.NgayXuat.ToString("dd/MM/yyyy").CompareTo(today) == 0).Count();
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            fn.TongHdxThang = db.Hdx.Where(x => x.NgayXuat.Month == month && x.NgayXuat.Year == year).Count();

            await db.SaveChangesAsync();
            return fn;
        }

        public async Task<List<DoanhThu>> GetTop5ByCategory()
        {
            var ds = new List<DoanhThu>(5);
            var data = await ((from c in db.Xe
                               where c.DaNgungBan == true && c.MaLoaiXe != null
                               group c by c.MaLoaiXe into g
                               orderby g.Count()
                               select new { MaLoaiXe = g.Key, Tong = g.Count() }).Take(5)).ToListAsync();
            foreach (var i in data)
            {
                var name = await db.LoaiXe.SingleOrDefaultAsync(l => l.MaLoaiXe == i.MaLoaiXe);

                ds.Add(new DoanhThu
                {
                    Name = name.TenLoaiXe,
                    Value = i.Tong
                });
            }
            return ds;
        }

        public async Task<List<DoanhThu>> GetTop3()
        {
            var ds = await (from c in db.CtHdx
                            group c by c.Xe.TenXe into g
                            orderby g.Count()
                            select new { TenXe = g.Key, Tong = g.Count() }).Take(3).ToListAsync();
            var result = new List<DoanhThu>();
            foreach (var i in ds)
            {
                result.Add(new DoanhThu
                {
                    Name = i.TenXe,
                    Value = i.Tong
                });
            }
            return result;
        }
    }
}