using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TechSolutions.Application.Helper;
using System.Security.Cryptography;
using TechSolutions.Domain.Interfaces;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Services;

public class TokenService(IConfiguration config, CryptHelper crypt, ITokenRepository tokenRepository) : ITokenService
{
    public async Task<TokenModel> GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(config["Auth:Key"]);

        var idEncrypt = crypt.Encrypt(user.Id.ToString());

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, idEncrypt)
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);

        var refreshToken = GenerateRefreshToken();

        var refreshTokenEncrypt = crypt.Encrypt(refreshToken);

        var tokenEntity = new Token(refreshToken, DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds(), user.Id);

        await tokenRepository.Add(tokenEntity);


        return new TokenModel
        (
            tokenString,
            DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds(),
            refreshTokenEncrypt,
            DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds()
        );
    }

    public async Task<Token> GetToken(string refreshToken)
    {
        var refreshTokenDecrypt = crypt.Decrypt(refreshToken);

        return await tokenRepository.Get(refreshTokenDecrypt);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        RandomNumberGenerator.Fill(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
