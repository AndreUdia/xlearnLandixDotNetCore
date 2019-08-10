using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dommel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using xlearn.Models;

namespace xlearn.Repository {
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity {

        protected readonly string ConnectionString;

        protected RepositoryBase(IConfiguration configuration) {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public virtual bool Delete(UInt64 id) {
            using(var db = new MySqlConnection(ConnectionString)) {
                var entity = GetById(id);

                if (entity == null) throw new Exception("Registro não encontrado");

                return db.Delete(entity);
            }
        }

        public virtual IEnumerable<TEntity> GetAll() {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.GetAll<TEntity>();
            }
        }

        public virtual TEntity GetById(UInt64 id) {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.Get<TEntity>(id);
            }
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate) {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.Select(predicate);
            }
        }

        public virtual void Insert(ref TEntity entity) {
            using(var db = new MySqlConnection(ConnectionString)) {
                var id = (UInt64) db.Insert(entity);

                entity = GetById(id);
            }
        }

        public virtual bool Update(TEntity entity) {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.Update(entity);
            }
        }
    }
}