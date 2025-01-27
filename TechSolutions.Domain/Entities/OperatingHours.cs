namespace TechSolutions.Domain.Entities;

public sealed class OperatingHours
{
    public int Id { get; init; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public ICollection<Room>? Rooms { get; private set; }

    private OperatingHours() { }

    public OperatingHours(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("O horário de início deve ser anterior ao horário de término.");

        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void ValidateOverlap(IEnumerable<OperatingHours> existingOperatingHours)
    {
        foreach (var existing in existingOperatingHours)
        {
            if (existing.DayOfWeek == this.DayOfWeek &&
                ((this.StartTime < existing.EndTime && this.StartTime >= existing.StartTime) ||
                 (this.EndTime > existing.StartTime && this.EndTime <= existing.EndTime)))
            {
                throw new InvalidOperationException("Já existe uma faixa de horário sobreposta para o mesmo dia.");
            }
        }
    }
}