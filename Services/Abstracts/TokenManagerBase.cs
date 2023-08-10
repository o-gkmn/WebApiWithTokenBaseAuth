using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public enum SecretKey
{
    RefreshTokenSecretKey,
    AccesTokenSecretKey
}

namespace Services.Abstracts
{
    public abstract class TokenManagerBase
    {
        private readonly IConfiguration _configuration;

        protected TokenManagerBase(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string RefreshTokenSecretKey => _configuration.GetSection("JWT")["AccesTokenKey"];
        public string AccesTokenSecretKey => _configuration.GetSection("JWT")["RefreshTokenKey"];

        public bool ValidateToken(string token, SecretKey key)
        {
            SecurityToken securityToken;
            
            ReadToken(token, key, out securityToken);

            var tokenRead = securityToken as JwtSecurityToken;

            var expTime = tokenRead.ValidTo;

            DateTime convertedExpTime = DateTime.SpecifyKind(
                    expTime,
                    DateTimeKind.Utc);

            var kind = expTime.Kind;


            if (DateTime.Now < expTime.ToLocalTime())
                return true;

            return false;
        }

        public ClaimsPrincipal? ReadToken(string token, SecretKey key, out SecurityToken securityToken)
        {
            var secretKey = DecideSecretKey(key);
            var splittedToken = token.Split('.');

            var signature = splittedToken[2];

            var headerAndPayload = $"{splittedToken[0]}.{splittedToken[1]}";
            var computedSignature = ComputeHmacSha256(secretKey, headerAndPayload);

            if (!string.Equals(signature, computedSignature))
            {
                Console.WriteLine("JWT signature validation failed.");
                securityToken = null;
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            securityToken = handler.ReadJwtToken(token);

            var jwtToken = securityToken as JwtSecurityToken;
            return new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
        }

        public string WriteToken(SecurityToken token, SecretKey key)
        {
            if (token == null) throw new ArgumentNullException();

            var securityToken = token as JwtSecurityToken;
            var secretKey = DecideSecretKey(key);

            var headerAndPayload = $"{securityToken.EncodedHeader}.{securityToken.EncodedPayload}";
            var signature = ComputeHmacSha256(secretKey, headerAndPayload);

            var jwtToken = $"{securityToken.EncodedHeader}.{securityToken.EncodedPayload}.{signature}";
            return jwtToken;
        }

        private string ComputeHmacSha256(string key, string data)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string? DecideSecretKey(SecretKey secretKey)
        {
            if (secretKey == SecretKey.AccesTokenSecretKey)
                return AccesTokenSecretKey;

            if (secretKey == SecretKey.RefreshTokenSecretKey)
                return RefreshTokenSecretKey;

            return null;
        }

    }
}
