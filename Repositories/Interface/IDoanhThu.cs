using System.Collections.Generic;
using System.Threading.Tasks;
using API.ViewModels;
using API.ViewModels.Chart;

namespace API.Repositories.Interface
{
    public interface IDoanhThu
    {
        Task<List<DoanhThu>> getAll();

        Task<List<DoanhThu>> GetTop5ByCategory();

        Task<FastNews> GetFastNews();

        Task<List<DoanhThu>> GetTop3();

    }
}