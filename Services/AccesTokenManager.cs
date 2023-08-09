using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstracts;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AccesTokenManager : TokenManagerBase, IAccesTokenManager
    {
        private readonly ILoggerService _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _manager;

        public AccesTokenManager(IConfiguration configuration, UserManager<User> manager, ILoggerService logger)
        {
            _configuration = configuration;
            _manager = manager;
            _logger = logger;
        }

        public async Task<string> GenerateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GetTokenOptions(signingCredentials, claims);

            var accesToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return accesToken;
        }

        public new bool ValidateToken(string token) => base.ValidateToken(token);

        public ClaimsPrincipal? GetPrincipal(string token)
        {
            var validationParamaters = GetValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, validationParamaters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogError($"token doesnt validate for {principal.Identity.Name}");
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public TokenValidationParameters GetValidationParameters()
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = jwtSettings["AccesTokenKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
            return tokenValidationParameters;
        }

        private SecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JWT");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["AccesTokenExpirationMinutes"])),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _manager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = jwtSettings["AccesTokenKey"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
