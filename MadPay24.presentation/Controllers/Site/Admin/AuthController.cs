using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MadPay24.Common.ErrorAndMessage;
using MadPay24.Data.DatabaseContext;
using MadPay24.Data.Dtos.Site.Admin;
using MadPay24.Data.Models;
using MadPay24.Repo.Infrastructure;
using MadPay24.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MadPay24.presentation.Controllers.Site.Admin
{
    [Authorize]
    [Route("site/admin/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="Site")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork<MadpayDbContext> dbcontext, IAuthService authService, IConfiguration configuration)
        {
            this._db = dbcontext;
            this._authService = authService;
            this._configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExist(userForRegisterDto.UserName))
            {
                return BadRequest(new ReturnMessage()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "این نام کاربری از قبل وجود دارد"
                });
            }

            var usercreate = new User()
            {
                Address = "",
                City = "",
                DateOfBirth = DateTime.Now,
                Gender = true,
                IsActive = false,
                Status = true,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                UserName = userForRegisterDto.UserName
            };

            var CreateUser = await _authService.Register(usercreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            userForLoginDto.UserName = userForLoginDto.UserName.ToLower();
            var userFromRepo = await _authService.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized("کاربری با این مشخصات وجود ندارد");
                //    new ReturnMessage()
                //{
                //    Status = false,
                //    Title = "خطا",
                //    Message = "کاربری با این مشخصات وجود ندارد"
                //});

            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id),
                new Claim(ClaimTypes.Name,userFromRepo.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokendes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = userForLoginDto.IsRemember ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokendes);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });


        }

        [AllowAnonymous]
        [HttpGet("getValue")]
        public async Task<IActionResult> GetValue()
        {

            return Ok(new ReturnMessage()
            {
                Status = true,
                Title = "ok",
                Message = "GetValue"
            });
        }


        [HttpGet("getValues")]
        public async Task<IActionResult> GetValues()
        {

            return Ok(new ReturnMessage()
            {
                Status = true,
                Title = "ok",
                Message = "GetValues"
            });


        }
    }
}