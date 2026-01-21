namespace MauiApp1.AppLogic.DTOs
{
    public record ProfileDto
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

    }
}
