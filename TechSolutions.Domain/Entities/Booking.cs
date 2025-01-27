using TechSolutions.Domain.Validation;

namespace TechSolutions.Domain.Entities;

public sealed class Booking
{
    public Guid Id { get; init; }
    public int RoomId { get; private set; }
    public Room? Room { get; private set; }
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public long StartAt { get; private set; }
    public long EndAt { get; private set; }
    public long CreatedAt { get; private set; }
    public long? UpdatedAt { get; private set; }
    public decimal Price { get; private set; }

    private Booking() { }
    public Booking(int roomId, Guid userId, long startAt, long endAt, decimal price)
    {
        ValidateDomain(roomId, userId, startAt, endAt);
        CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Price = price;
    }

    public void Update(int roomId, Guid userId, long startAt, long endAt, decimal price)
    {
        ValidateDomain(roomId, userId, startAt, endAt);
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Price = price;
    }

    private void ValidateDomain(int roomId, Guid userId, long startAt, long endAt)
    {
        DomainExceptionValidation.When(startAt > endAt, "A data de início deve ser menor que a data de fim");
        DomainExceptionValidation.When(roomId <= 0, "Sala inválida");
        DomainExceptionValidation.When(userId == Guid.Empty, "Usuário inválido");

        RoomId = roomId;
        UserId = userId;
        StartAt = startAt;
        EndAt = endAt;
    }
}
