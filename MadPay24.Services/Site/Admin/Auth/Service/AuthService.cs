using MadPay24.Common.Helper;
using MadPay24.Data.DatabaseContext;
using MadPay24.Data.Models;
using MadPay24.Repo.Infrastructure;
using MadPay24.Services.Site.Admin.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Services.Site.Admin.Auth.Service
{
    public class AuthService: IAuthService
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        public AuthService(IUnitOfWork<MadpayDbContext> dbcontext)
        {
            _db = dbcontext;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _db.UserRepository.GetAsync(p => p.UserName == username);
            
            if (user == null)
                return null;

            if (!Utilities.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordhash, passwordsalt;
            Utilities.CreatePaswordHash(password, out passwordhash, out passwordsalt);

            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordsalt;

            await _db.UserRepository.InsertAsync(user);
            await _db.SaveAsync();

            return user;
        }
    }
}
