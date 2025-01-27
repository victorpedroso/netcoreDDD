using TechSolutions.Application.Models;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Application.Mappings;

public static class UserMapper
{
    public static UserModel ToModel(this User user) =>
    
        new(user.Name, user.Email, user.Cpf, user.Role.Name, user.Phone);
   

    public static User ToEntity(this CreateUserModel user, int? roleId = null) =>
   
        new(user.Name, user.Email, user.Cpf, user.Password, user.Phone, roleId);
    

    public static User ToEntity(this UserModel userModel) =>

        new(userModel.Name, userModel.Cpf, userModel.Email, string.Empty, userModel.Phone);
    

    public static IEnumerable<UserModel> ToModelList(this IEnumerable<User> usersList) =>

        usersList.Select(ToModel).ToList();
    

    public static IEnumerable<User> ToEntityList(this IEnumerable<UserModel> usersList) =>
        usersList.Select(ToEntity).ToList();
    
}
