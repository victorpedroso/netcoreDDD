using TechSolutions.Application.Helper;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Mappings;
using TechSolutions.Application.Models;
using TechSolutions.Domain.Interfaces;

namespace TechSolutions.Application.Services;

public class AuthService(IUserRepository userRepository, ITokenService tokenService) : IAuthService
{
    public async Task<Result<TokenModel>> Auth(AuthModel auth)
    {
        var user = await userRepository.GetByEmail(auth.Email);

        if (user == null)
            return Result<TokenModel>.IsError("Usuário não encontrado");

        bool validPassword = auth.Password.VerifyPassword(user.Password);

        if (!validPassword)
            return Result<TokenModel>.IsError("Usuário ou senha inválidos");

        var token = await tokenService.GenerateToken(user);

        return Result<TokenModel>.IsSuccess(token);
            
    }

    public async Task<Result<TokenModel>> ReAuth(string refreshToken)
    {
        var tokenModel = await tokenService.GetToken(refreshToken);

        if (tokenModel == null) return Result<TokenModel>.IsError("Token inválido");

        var user = await userRepository.GetById(tokenModel.UserId);

        var token = await tokenService.GenerateToken(user);

        return Result<TokenModel>.IsSuccess(token);
    }
}
