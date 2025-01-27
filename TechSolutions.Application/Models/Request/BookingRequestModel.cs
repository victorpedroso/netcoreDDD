using System.ComponentModel.DataAnnotations;

namespace TechSolutions.Application.Models.Request;

public class BookingRequestModel
{
    [Required(ErrorMessage = "Id da sala é obrigatório")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Data de início obrigatória")]
    public long StartAt { get; set; }

    [Required(ErrorMessage = "Data de fim obrigatória")]
    public long EndAt { get; set; }
    public decimal? Price { get; set; }
}
