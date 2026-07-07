using Microsoft.IdentityModel.Tokens;
using MMAC.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MMAC.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public TokenService(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<string> CreateToken(Guid travellerId)
        {
            var traveller = await _context.Traveller.FindAsync(travellerId);
            if (traveller == null) throw new Exception("Traveller not found");

            var jwtKey = _config["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is missing");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim("id",                        traveller.TravellerId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, traveller.FullName),
                new Claim("PassportNo",                traveller.PassportNo),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = creds,

                // must match ValidIssuer / ValidAudience in Program.cs ──
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
