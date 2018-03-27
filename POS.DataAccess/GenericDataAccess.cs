using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace POS.DataAccess
{
    public class GenericDataAccess<T> where T:class
    {
        private DbContext _dbContext;
        private DbSet<T> _targetTable;

        public GenericDataAccess(DbContext db)
        {
            _dbContext = db;
            _targetTable = _dbContext.Set<T>();
        }

        public virtual bool Add(T entity)
        {
            _targetTable.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual ICollection<T> GetAll()
        {
            return _targetTable.ToList();
        }

        public virtual T GetSingle(Expression<Func<T, bool>> conditionExpression)
        {
            return (T)_targetTable.FirstOrDefault(conditionExpression);
        }

        public bool Update(T entity)
        {
            _targetTable.Attach(entity);
            _dbContext.Entry(entity).State=EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool Delete(T entity)
        {
            _targetTable.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
