using System.ComponentModel.DataAnnotations;

namespace TechSolutions.Application.Models;

public class CreateUserModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 caracteres")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string? Password { get; set; }
    public string? Phone { get; set; }
}
