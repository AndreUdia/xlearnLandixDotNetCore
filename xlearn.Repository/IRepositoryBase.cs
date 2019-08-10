using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using xlearn.Models;

namespace xlearn.Repository {
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(UInt64 id);
        void Insert(ref TEntity entity);
        bool Update(TEntity entity);
        bool Delete(UInt64 id);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
    }
}
