using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Repo.Infrastructure
{
    public abstract class Repository<TEntity>:IRepository<TEntity>,IDisposable where TEntity:class
    {
        

        #region ctor
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        #endregion

        #region normal 
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("there is no entity");
            _dbSet.Update(entity);
        }
        public void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null)
                throw new ArgumentException("there is no entity");
            _dbSet.Remove(entity);
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objs = _dbSet.Where(where).AsEnumerable();
            foreach (TEntity item in objs)
            {
                _dbSet.Remove(item);
            }
        }
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<TEntity> GetMany(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeEntity = "", int count = 0)
        {
            //return _dbSet.Where(where).FirstOrDefault();

            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeentity in includeEntity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeentity);
            }

            count = Math.Abs(count);
            if (count > 0)
                query = query.Take(count);

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }
        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.AsNoTracking().Where(where).FirstOrDefault();
        }

        public bool IsAny(Expression<Func<TEntity, bool>> filter)
        {
            if (_dbSet.Any(filter))
                return true;
            return false;

        }
        public async Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (await _dbSet.AnyAsync(filter))
                return true;
            return false;
        }
        #endregion

        #region async
        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetManyAsync(
      Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeEntity = "", int count = 0)
        {
            //return _dbSet.Where(where).FirstOrDefault();

            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeentity in includeEntity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeentity);
            }

            count = Math.Abs(count);
            if (count > 0)
                query = query.Take(count);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public async Task<long> GetCountAsync(
      Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();


        }
        public async Task<long> GetSumAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, long>> select = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (select == null)
            {
                return 0;
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.SumAsync(select);


        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.AsNoTracking().Where(where).FirstOrDefaultAsync();
        }

        #endregion

        #region dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TEntity>> GetManyAsyncPaging(Expression<Func<TEntity,
            bool>> filter, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy,
            string includeEntity, int count, int firstCount, int page)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeentity in includeEntity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeentity);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.Skip(firstCount).Skip(count * page).Take(count).ToListAsync();



        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion
    }
}
