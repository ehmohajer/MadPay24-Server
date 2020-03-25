using MadPay24.Data.DatabaseContext;
using MadPay24.Repo.Infrastructure;
using MadPay24.Data.Models;
using MadPay24.Repo.Repositories.Interface;
using MadPay24.Common.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Repo.Repositories.Repo
{
    public class UserRepository: Repository<User>,IUserRepository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext dbContext):base(dbContext)
        {
            this._db=(_db ?? (MadpayDbContext)_db);
        }

        

        public async Task<bool> UserExist(string username)
        {
            if (await GetAsync(p => p.UserName == username) != null)
                return true;

            return false;
        }
    }
}
