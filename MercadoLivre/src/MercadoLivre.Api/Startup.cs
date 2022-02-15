using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoLivre.Api.Config;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository;
using MercadoLivre.Service.Notifications;
using Microsoft.EntityFrameworkCore;
using MercadoLivre.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MercadoLivre.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt
                .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
            );

            services.AddAutoMapper(typeof(Startup));
            services.addDI();
            services.AddAPIConfig();
            services.addJWTConfig();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAPIConfig(env);
        }
    }
}
