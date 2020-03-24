using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Data.Infrastructure
{
    public interface IUnitOfWork<TContext>:IDisposable where TContext:DbContext
    {
        void Save();
        Task<int> SaveAsync();
    }
}
