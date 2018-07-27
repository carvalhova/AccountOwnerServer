using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AccountOwnerServer.Infra;
using AccountOwnerServer.Infra.Filters;
using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceCollectionEntensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        //.WithOrigins("http://www.something.com")
                    );
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                //options.AutomaticAuthentication = true;
                //options.AuthenticationDisplayName = null;
                //options.ForwardClientCertificate = true;
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlServerDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SqlServerConnection:ConnectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();
        }

        public static void ConfigureJwt(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The issuer is the actual server that created the token
                        ValidateIssuer = true,

                        // The receiver of the token is a valid recipient
                        ValidateAudience = true,

                        // The token has not expired
                        ValidateLifetime = true,

                        // The signing key is valid and is trusted by the server
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "http://localhost:5000",
                        ValidAudience = "http://localhost:5000",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurity.Key))
                    };
                });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("default-policy", b =>
                {
                    b.RequireAuthenticatedUser();
                });
                o.AddPolicy("api-policy", b =>
                {
                    b.RequireAuthenticatedUser();
                    b.RequireClaim(ClaimTypes.Role, "Access.Api");
                    b.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });
            });
        }
    }
}