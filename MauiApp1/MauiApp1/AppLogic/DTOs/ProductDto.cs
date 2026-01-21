namespace MauiApp1.AppLogic.DTOs
{
    public record ProductDto
    {
        public string Id { get; init; } = string.Empty;

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public decimal Price { get; init; }

        public int StockQuantity { get; init; }

        public string ImageUrl { get; init; } = string.Empty;

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

        public string Status { get; init; } = string.Empty;
    }
}
