using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadPay24.Data.DatabaseContext;
using MadPay24.Repo.Infrastructure;
using MadPay24.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MadPay24.Services.Site.Admin.Auth.Interface;

namespace MadPay24.presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
        private readonly IAuthService _authService;
        public WeatherForecastController(IUnitOfWork<MadpayDbContext> dbcontext, IAuthService authService)
        {
            this._db = dbcontext;
            this._authService = authService;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            //var rng = new Random();
            //return  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            var user = new User()
            {
                Address="iran",
                City="mashhad",
                Gender= true,
                IsActive= false,
                Name= "ehsan",
                PhoneNumber= "",
                Status= true,
                UserName= "eh.mohajer",
                PasswordHash= new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, },
                PasswordSalt= new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, }
            };

            //await _db.UserRepository.InsertAsync(user);
            //await _db.SaveAsync();

            //var model = await _db.UserRepository.GetAllAsync();

            var model= await _authService.Register(user, "qwerty");

            return Ok(model);
        }
    }
}
