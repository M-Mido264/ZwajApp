using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using ZwajApp.Data;
using System.Text;

namespace ZwajApp
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
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors();
            services.AddScoped<IAuthRepository,AuthRepository>();
            //authentication middelware
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(Options=>{
                Options.TokenValidationParameters = new TokenValidationParameters{
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts(); //related to security
            }

           // app.UseHttpsRedirection();
            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();//always last line
        }

    }
}
