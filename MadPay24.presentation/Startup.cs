using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MadPay24.Common.Helper;
using MadPay24.Data.DatabaseContext;
using MadPay24.Repo.Infrastructure;
using MadPay24.Services.Seed.Interface;
using MadPay24.Services.Seed.Service;
using MadPay24.Services.Site.Admin.Auth.Interface;
using MadPay24.Services.Site.Admin.Auth.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace MadPay24.presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddScoped<IUnitOfWork<MadpayDbContext>, UnitOfWork<MadpayDbContext>>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddTransient<ISeedService, SeedService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //document
            services.AddOpenApiDocument(document =>
            {
            document.DocumentName = "Site";
            document.ApiGroupNames = new[] { "Site" };

            document.PostProcess = d =>
            {
                d.Info.Title = "Hello world!";
                d.Info.Contact = new NSwag.OpenApiContact
                {
                    Name = "Shayne Boyer",
                    Email = string.Empty,
                    Url = ""
                };
                d.Info.License = new NSwag.OpenApiLicense
                {
                    Name = "Shayne Boyer",
                    Url = ""
                };
            };

            document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into textbox: Bearer {your Jwt token}"
            });

            document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            //new OperationSecurityScopeProcessor("bearer"));



            //document.OperationProcessors.Add(
            //    new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "Api";
                document.ApiGroupNames = new[] { "Api" };
});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedService seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                             context.Response.AddAppError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            //app.UseHttpsRedirection();
            //seed.SeedUsers();
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi(); // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
