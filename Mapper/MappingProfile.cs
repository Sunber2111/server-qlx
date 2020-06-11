using API.Models;
using API.ViewModels;
using AutoMapper;

namespace API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NhanVien, NhanVienDTO>();


            CreateMap<BaoHanh, BaoHanhDTO>();

            CreateMap<Hdn, HdnDTO>()
            .ForMember(c => c.TenNcc, o => o.MapFrom(t => t.Ncc.TenNcc))
                    .ForMember(c => c.TenNv, o => o.MapFrom(t => t.NhanVien.TenNv));

            CreateMap<Hdx, HdxDTO>()
                    .ForMember(c => c.TenKh, o => o.MapFrom(t => t.KhachHang.TenKh))
                    .ForMember(c => c.TenNv, o => o.MapFrom(t => t.NhanVien.TenNv));

            CreateMap<KhachHang, KhachHangDTO>();

            CreateMap<Kho, KhoDTO>()
                        .ForMember(c => c.CtKho, o => o.MapFrom(t => t.CtKho));

            CreateMap<LoaiXe, LoaiXeDTO>();

            CreateMap<Ncc, NccDTO>();

            CreateMap<CtKho, CtKhoDTO>()
                        .ForMember(c => c.Xe, o => o.MapFrom(t => t.Xe));

            CreateMap<Xe, XeDTO>()
                .ForMember(c => c.LoaiXe, o => o.MapFrom(t => t.LoaiXe.TenLoaiXe))
                .ForMember(c => c.MaCtKho, o => o.MapFrom(t => t.CtKho.Id))
                .ForMember(c => c.TenKho, o => o.MapFrom(t => t.CtKho.Kho.TenKho));

            CreateMap<CtHdx, CtHdxDTO>()
                    .ForMember(c => c.TenXe, o => o.MapFrom(t => t.Xe.TenXe))
                    .ForMember(c => c.TenKho, o => o.MapFrom(t => t.Kho.TenKho));
            CreateMap<CtHdn, CtHdnDTO>()
                    .ForMember(c => c.TenXe, o => o.MapFrom(t => t.Xe.TenXe))
                    .ForMember(c => c.TenKho, o => o.MapFrom(t => t.Kho.TenKho));
        }
    }
}
