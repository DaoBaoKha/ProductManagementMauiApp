namespace MauiApp1.AppLogic.DTOs
{
    public record ProductDto
    {
        public string Id { get; init; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = string.Empty;
    }
}
