using JwtTest2.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest2.Services
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }

    }

    public interface IJwtService
    {
        JwtToken GenerateToken(string UserID);
        TokenValidationParameters GetTokenValidationParams();
        UserAuth GetUser(ClaimsPrincipal User);
    }

    public class JwtService : IJwtService
    {
        private string _key;

        public JwtService(string Key)
        {
            _key = Key;
        }

        public static readonly string USERID_CLAIM = "UserID";

        public UserAuth GetUser(ClaimsPrincipal User)
        {
            var UserID = User.Claims.FirstOrDefault(x => x.Type == USERID_CLAIM)?.Value;

            if (UserID == null)
                return null;

            return new UserAuth
            {
                Email = UserID
            };
        }

        public TokenValidationParameters GetTokenValidationParams()
        {
            try
            {
                return new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                    ClockSkew = TimeSpan.Zero
                };
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JwtToken GenerateToken(string UserID)
        {
            try
            {
                var claims = new[]
             {
                    new Claim(JwtService.USERID_CLAIM, UserID),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddMinutes(5);

                var token = new JwtSecurityToken(
                   issuer: null,
                   audience: null,
                   claims: claims,
                   expires: expiration,
                   signingCredentials: creds);

                var Token =  new JwtSecurityTokenHandler().WriteToken(token);

                return new JwtToken
                { 
                    ExpireDate = expiration,
                    Token = Token
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
