using System.IdentityModel.Tokens.Jwt;

namespace Services.Abstracts
{
    public abstract class TokenManagerBase
    {
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenRead = tokenHandler.ReadJwtToken(token);

            var expTime = tokenRead.ValidTo;

            DateTime convertedExpTime = DateTime.SpecifyKind(
                    expTime,
                    DateTimeKind.Utc);

            var kind = expTime.Kind;


            if (DateTime.Now < expTime.ToLocalTime())
                return true;

            return false;
        }
    }
}
