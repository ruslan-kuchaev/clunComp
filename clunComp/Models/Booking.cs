namespace clunComp.Models;

public class Booking
{
    public int Id { get; set; }
    public int ComputerId { get; set; }
    public Computer? Computer { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserPhone { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public int DurationHours { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public enum BookingStatus
{
    Pending,      // Ожидает подтверждения
    Confirmed,    // Подтверждено
    Cancelled,    // Отменено
    Completed     // Завершено
}
