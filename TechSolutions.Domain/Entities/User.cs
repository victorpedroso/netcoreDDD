using TechSolutions.Domain.Enum;
using TechSolutions.Domain.Validation;

namespace TechSolutions.Domain.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Cpf { get; private set; }
    public string? Password { get; private set; }
    public StatusEnum Status { get; private set; }
    public int? RoleId { get; private set; }
    public Role? Role { get; private set; }
    public long DateCreated { get; init; }
    public long? DateUpdated { get; private set; }
    public string? Phone { get; private set; }

    public Token? Token { get; private set; }

    public User(string name, string email, string cpf, string password, string phone, int? roleId = null)
    {
        Id = Guid.NewGuid();
        Status = StatusEnum.Active;
        DateCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        RoleId = roleId;
        ValidateDomain(name, email, cpf, password, phone);
    }

    public User(Guid id, string name, string email, string cpf, string password, string phone, int? roleId = null)
    {
        Id = id;
        Status = StatusEnum.Active;
        RoleId = roleId;
        ValidateDomain(name, email, cpf, password, phone);
    }

    public void Update(string name, string email, string cpf, string password, bool active, string phone)
    {
        ValidateDomain(name, email, cpf, password, phone);
    }

    public void UpdatePassword(string password) => Password = password;
    private void ValidateDomain(string name, string email, string cpf, string password, string phone)
    {
        DomainExceptionValidation.When(name.Length > 100, "O nome deve ter no máximo caracteres");
        DomainExceptionValidation.When(email.Length > 100, "O e-mail deve no máximo 100 caracteres");
        DomainExceptionValidation.When(cpf.Length >11, "O cpf deve ter no máximo 11 caracteres");

        Name = name;
        Email = email;
        Cpf = cpf;
        Password = password;
        Phone = phone;
    }
}
