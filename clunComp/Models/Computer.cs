namespace clunComp.Models;

public class Computer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Specifications { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public string Zone { get; set; } = string.Empty; // Общая Зона, Лучшие Места, Диваны + PS5
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
}
