using MadPay24.Repo.Repositories.Interface;
using MadPay24.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Repo.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext,new()
    {
        protected readonly DbContext _db;
        public UnitOfWork()
        {
            _db = new TContext();
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return await _db.SaveChangesAsync();
        }

        //-----------TEntitity Repository Register--------
        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_db);
                return userRepository;
            }
        }

        //-----------Dispose--------
        private bool disposed=false;

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

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
