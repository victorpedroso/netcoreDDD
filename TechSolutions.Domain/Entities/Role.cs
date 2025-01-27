using TechSolutions.Domain.Validation;

namespace TechSolutions.Domain.Entities;

public sealed class Role
{
    public int Id { get; init; }
    public string? Name { get; private set; }
    public long DateCreated { get; init; }
    public long? DateUpdated { get; set; }
    public ICollection<User>? Users { get; private set; }

    public Role(string name)
    {
        DateCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        ValidateDomain(name);
    }

    public Role(int id, string name)
    {
        Id = id;
        DateCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(name.Length > 100, "O nome deve ter até 100 caracteres");

        Name = name;
    }
}
