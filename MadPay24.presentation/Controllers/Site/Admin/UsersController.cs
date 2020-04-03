using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadPay24.Data.DatabaseContext;
using MadPay24.Repo.Infrastructure;
using MadPay24.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MadPay24.presentation.Controllers.Site.Admin
{
    [Authorize]
    [Route("site/admin/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Site")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        private readonly IConfiguration _configuration;

        public UsersController(IUnitOfWork<MadpayDbContext> dbcontext, IConfiguration configuration)
        {
            this._db = dbcontext;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.UserRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _db.UserRepository.GetByIdAsync(id);
            return Ok(user);
        }
    }
}