using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IUserService
{
    Task<Result<TokenModel>> Auth(AuthModel auth);
    Task<Result<TokenModel>> ReAuth(string refreshToken);
    Task<Result<UserModel>> MeInfo(string id);
    Task<Result<UserModel>> Create(CreateUserModel model);
    Task<Result<bool>> ForgotPassword(string email);
    Task<Result<bool>> ChangePassword(ChangePasswordModel changePasswordModel);
    Task<Result<bool>> Update(string id, CreateUserModel model);
    Task<Result<bool>> Delete(string id);
}
