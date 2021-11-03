using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using TuRutaUN.Entities.Data;
using TuRutaUN.Entities.Login;
using TuRutaUN.Helpers;
using TuRutaUN.Servicies;

namespace TuRutaUN
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
            // Add data context to services
            services.AddDbContext<TuRutaUNContext>();
            services.AddDbContext<ExternalLoginDBContext>();
            // Add cors to services
            services.AddCors();

            services.AddControllers();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettigns>(appSettingsSection);

            // Configure JWT authentication
            var appSettings = appSettingsSection.Get<AppSettigns>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication( authOptions => 
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( jwtBearerOptions => 
            {
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var dataUserService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var dataUserId = (context.Principal.Identity.Name).ToString();
                        var user = dataUserService.GetById(dataUserId);
                        // Return unauthorized if user no longer exists
                        if (user == null) context.Fail("Unauthorized");

                        return Task.CompletedTask;
                    }
                };

                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            // Register the Swagger generator, defining 1 or more Swagger documents

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TuRutaUN", Version = "v1" });
            });

            // Add Automapper to services
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Add dependency injection
            services.AddScoped<ILoginService,LoginService>();
            services.AddScoped<IUserService,UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TuRutaUN v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
