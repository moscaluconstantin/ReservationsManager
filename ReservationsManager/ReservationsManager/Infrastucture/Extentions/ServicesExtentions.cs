using EFCoreMappingApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.BLL.Services;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.DAL.Repositories;
using System.Text;

namespace ReservationsManager.API.Extentions
{
    public static class ServicesExtentions
    {
        public static void AddDbContexts(this IServiceCollection services, ConfigurationManager configuration)
        {
            string connectionString = configuration.GetConnectionString("myDb1");
            services.AddSingleton(x => new RezervationsDbContext(connectionString));

            // For Entity Framework
            services.AddDbContext<RezervationsDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IActionEmployeesRepository, ActionEmployeesRepository>();
            services.AddScoped<ITimeBlocksRepository, TimeBlocksRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IActionEmployeesService, ActionEmployeesService>();
            services.AddScoped<IReservationsService, ReservationsService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSecretKey = configuration.GetValue<string>("Jwt:SecretKey");
            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
