using TechSolutions.Domain.Validation;

namespace TechSolutions.Domain.Entities;

public sealed class Room
{
    public int Id { get; init; }
    public string? Name { get; private set; }
    public ICollection<OperatingHours>? OperatingHours { get; private set; }
    public long CreatedAt { get; private set; }
    public long? UpdatedAt { get; private set; }

    private Room() { }

    public Room(string name, IEnumerable<OperatingHours> operatingHours)
    {
        ValidateDomain(name);
        OperatingHours = operatingHours.ToList();
        CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public void Update(string name, IEnumerable<OperatingHours> operatingHours)
    {
        ValidateDomain(name);
        OperatingHours = operatingHours.ToList();
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(name.Length > 100, "O nome deve ter no máximo 100 caracteres");
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "O nome deve ter no máximo 100 caracteres");
        Name = name;
    }

}
