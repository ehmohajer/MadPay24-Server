using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay24.Services.Seed.Interface
{
    public interface ISeedService
    {
        void SeedUsers();
        Task SeedUsersAsync();
    }
}
