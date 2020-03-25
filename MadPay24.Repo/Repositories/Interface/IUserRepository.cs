using MadPay24.Data.Models;
using MadPay24.Repo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Repo.Repositories.Interface
{
    public interface IUserRepository:IRepository<User>
    {
        
        Task<bool> UserExist( string username);
    }
}
