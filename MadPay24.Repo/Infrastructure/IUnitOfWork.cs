using MadPay24.Repo.Repositories.Interface;
using MadPay24.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext>:IDisposable where TContext:DbContext
    {
        IUserRepository UserRepository { get; }

        void Save();
        Task<int> SaveAsync();
    }
}
