using TechSolutions.Application.Helper;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Mappings;
using TechSolutions.Application.Models;
using TechSolutions.Domain.Interfaces;

namespace TechSolutions.Application.Services;

public class UserService(IUserRepository userRepository, IRoleRepository roleRepository, IAuthService authService, CryptHelper cryptHelper, IEmailService emailService, IRedisService<StorePasswordModel> redisService) : IUserService
{
    public async Task<Result<TokenModel>> Auth(AuthModel auth)
    {
        return await authService.Auth(auth);
    }

    public async Task<Result<TokenModel>> ReAuth(string refreshToken)
    {
        return await authService.ReAuth(refreshToken);
    }

    public async Task<Result<UserModel>> MeInfo(string id)
    {
        var idDecrypt = cryptHelper.Decrypt(id);

        var user = await userRepository.GetById(new Guid(idDecrypt));

        if (user == null)
            return Result<UserModel>.IsError("Token inválido");

        return Result<UserModel>.IsSuccess(user.ToModel());
    }

    public async Task<Result<UserModel>> Create(CreateUserModel model)
    {
        if (await userRepository.GetByEmail(model.Email) != null)
            return Result<UserModel>.IsError("Já existe cadastro para o e-mail informado");

        if (await userRepository.GetByCpf(model.Cpf) != null)
            return Result<UserModel>.IsError("Verifique os dados e tente novamente");

        var clientRole = await roleRepository.GetByName("Client");

        if (clientRole == null)
            return Result<UserModel>.IsError("Role não encontrada");

        var user = model.ToEntity(clientRole.Id);
        user.UpdatePassword(model.Password.HashPassword());

        var createdUser = await userRepository.Add(user);

        return Result<UserModel>.IsSuccess(createdUser.ToModel());
    }

    public async Task<Result<bool>> ForgotPassword(string email)
    {
        var user = await userRepository.GetByEmail(email);

        if (user == null) return Result<bool>.IsError("Usuário não encontrado");

        var code = GenerateVerificationCode();

        var emailSend = CreatePasswordResetEmail(email, code);

        //if (!emailService.Send(emailSend))
        //    return Result<bool>.IsError("Erro ao redefinir a senha");

        var storePassword = CreateStorePassword(user.Id, code);

        var createStorePassword = await redisService.Update(user.Cpf, storePassword, TimeSpan.FromMinutes(10));

        if (!createStorePassword.Success) return Result<bool>.IsError("Erro ao redefinir a senha");

        return Result<bool>.IsSuccess(true);
    }

    public async Task<Result<bool>> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        var user = await userRepository.GetByEmail(changePasswordModel.Email);

        if (user == null) return Result<bool>.IsError("Usuário não encontrado");

        var storeCode = await redisService.Get(user.Cpf);

        if (!storeCode.Success) return Result<bool>.IsError("Dados inválidos");

        if (user.Id == storeCode.Data.UserId && changePasswordModel.Code == storeCode.Data.Code)
        {
            user.UpdatePassword(changePasswordModel.NewPassword.HashPassword());

            await userRepository.Update(user);

            await redisService.Delete(user.Cpf);

            return Result<bool>.IsSuccess(true);
        }

        return Result<bool>.IsError("Dados inválidos.");

    }

    public async Task<Result<bool>> Update(string id, CreateUserModel model)
    {
        var idDecrypt = cryptHelper.Decrypt(id);

        var user = await userRepository.GetById(new Guid(idDecrypt));

        if (user == null)
            return Result<bool>.IsError("Usuário não encontrado");

        await userRepository.Update(user);

        return Result<bool>.IsSuccess(true);
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var idDecrypt = cryptHelper.Decrypt(id);

        var user = await userRepository.GetById(new Guid(idDecrypt));

        if (user == null)
            return Result<bool>.IsError("Usuário não encontrado");

        await userRepository.Delete(user);

        return Result<bool>.IsSuccess(true);
    }

    private static int GenerateVerificationCode() => new Random().Next(100000, 1000000);

    private static SendEmailModel CreatePasswordResetEmail(string email, int code)
    {
        return new SendEmailModel
        {
            Email = email,
            Subject = "Tech Solutions - Recuperação de senha",
            Body = $"Digite o código: {code} no app para redefinir sua senha\nEsse código expira em 10 minutos"
        };
    }

    private static StorePasswordModel CreateStorePassword(Guid userId, int code)
    {
        return new StorePasswordModel
        {
            UserId = userId,
            Code = code
        };
    }
}
