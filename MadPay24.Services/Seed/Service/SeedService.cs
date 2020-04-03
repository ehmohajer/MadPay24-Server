using MadPay24.Common.Helper;
using MadPay24.Data.DatabaseContext;
using MadPay24.Data.Models;
using MadPay24.Repo.Infrastructure;
using MadPay24.Services.Seed.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Services.Seed.Service
{
    public class SeedService : ISeedService
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        public SeedService(IUnitOfWork<MadpayDbContext> dbcontext)
        {
            _db = dbcontext;
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Files/Jason/Seed/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            byte[] passwordhash, passwordsalt;
            Utilities.CreatePaswordHash("123456", out passwordhash, out passwordsalt);

            foreach (var user in users)
            {
                user.PasswordHash = passwordhash;
                user.PasswordSalt = passwordsalt;
                user.UserName = user.UserName.ToLower();
                _db.UserRepository.Insert(user);
            }
            _db.Save();
        }

        public async Task SeedUsersAsync()
        {
            var userData = await System.IO.File.ReadAllTextAsync("Files/Jason/Seed/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            byte[] passwordhash, passwordsalt;
            Utilities.CreatePaswordHash("123456", out passwordhash, out passwordsalt);

            foreach (var user in users)
            {
                user.PasswordHash = passwordhash;
                user.PasswordSalt = passwordsalt;
                user.UserName = user.UserName.ToLower();
                await _db.UserRepository.InsertAsync(user);
            }
            await _db.SaveAsync();
        }
    }
}
