using ddd.Domain.Entities;
using ddd.Domain.Interfaces;
using ddd.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace ddd.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _mySqlContext;
        public BaseRepository(ApplicationDbContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public void Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }
        public void Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }
        public void Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }
        public IList<TEntity> Select() =>
            _mySqlContext.Set<TEntity>().ToList();
        public TEntity Select(int id) =>
            _mySqlContext.Set<TEntity>().Find(id);
    }
}
