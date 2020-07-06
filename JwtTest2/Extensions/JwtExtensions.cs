using JwtTest2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JwtTest2.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwt(this IServiceCollection services, IJwtService service)
        {
            try
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = service.GetTokenValidationParams();
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
