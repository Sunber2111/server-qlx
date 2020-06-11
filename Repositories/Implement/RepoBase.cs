using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Models;
using API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Implement
{
    public class RepoBase<Model, ViewModel> : IRepoBase<Model, ViewModel>
        where Model : class
        where ViewModel : class
    {
        protected readonly DataContext db;

        protected readonly IMapper mapper;

        public RepoBase(IServiceProvider serviceProvider)
        {
            db = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()
                .ServiceProvider.GetRequiredService<DataContext>();
            mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var item = await db.Set<Model>().FindAsync(id);
                if (item == null)
                    throw new Exception("Không Tồn Tại");
                db.Set<Model>().Remove(item);
                if (await db.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewModel>> GetAll()
        {
            try
            {
                var ds = await db.Set<Model>().ToListAsync();
                return mapper.Map<List<Model>, List<ViewModel>>(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ViewModel> GetById(object id)
        {
            try
            {
                var item = await db.Set<Model>().FindAsync(id);
                if (item == null)
                    throw new Exception("Không Tồn Tại");
                return mapper.Map<Model, ViewModel>(item);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ViewModel> GetFilter(Func<Model, bool> filter)
        {
            try
            {
                var ds = db.Set<Model>().Where(filter).ToList();
                return mapper.Map<List<Model>, List<ViewModel>>(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ViewModel> Insert(Model obj)
        {
            try
            {
                db.Set<Model>().Add(obj);
                await db.SaveChangesAsync();
                return mapper.Map<Model, ViewModel>(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Update(Model obj)
        {
            try
            {
                db.Entry(obj).State = EntityState.Modified;
                if (await db.SaveChangesAsync() > 0)
                    return true;
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
