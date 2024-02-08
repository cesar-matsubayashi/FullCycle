// Back-end Challenge
// Write a simple class called MainClass with a method string GenerateJwtWithFixedClaims(string secret, string issuer, string audience, string sub, string jti, long iat) that takes a secret, issuer, audience, subject, JWT ID, and issued at time as parameters and generates a JWT with the following claims:

// 1. sub (subject) with the provided value.
// 2. jti (JWT ID) with the provided value.
// 3. iat (issued at) with the provided Unix timestamp value.

// The JWT should not have an expiration claim and should be signed using the HMAC SHA-256 algorithm.

// Example Input:
// GenerateJwtWithFixedClaims("your-secret-key-1234", "your-issuer", "your-audience", "sub-value-1", "jti-value-1", 1626300000);

// Example Output:
// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdWItdmFsdWUtMSIsImp0aSI6Imp0aS12YWx1ZS0xIiwiaWF0IjoxNjI2MzAwMDAwLCJpc3MiOiJ5b3VyLWlzc3VlciIsImF1ZCI6InlvdXItYXVkaWVuY2UifQ.MLB4TiTE40ps89RPesxASz0SzUMe_i3NDmmykZiv1ps

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtGenerator
{
    public class MainClass
    {
        public string Generate(string secret, string issuer, string audience, string sub, string jti, long iat)
        {
            // Convert the Unix timestamp to a .NET DateTime
            DateTimeOffset now = DateTimeOffset.FromUnixTimeSeconds(iat);

            // Create a new JWT security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Issuer, issuer),
                    new Claim(ClaimTypes.Audience, audience),
                    new Claim(ClaimTypes.Name, sub),
                    new Claim(ClaimTypes.NameIdentifier, jti)
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = now,
                Id = jti
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Get the JWT string
            return tokenHandler.WriteToken(token);
        }
    }
}