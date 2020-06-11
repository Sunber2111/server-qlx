using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IRepoBase<Model,ViewModel> 
        where Model : class 
        where ViewModel : class
    {
        Task<List<ViewModel>> GetAll();

        List<ViewModel> GetFilter(Func<Model, bool> filter);

        Task<ViewModel> GetById(object id);

        Task<ViewModel> Insert(Model obj);

        Task<bool> Update(Model obj);

        Task<bool> Delete(object id);

        // trả về Task thì là hàm bất đồng bộ
    }
}
