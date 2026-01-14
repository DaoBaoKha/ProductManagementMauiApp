namespace MauiApp1.AppLogic.DTOs;

/// <summary>
/// Example DTO for User - used to transfer data to/from UI
/// </summary>
public record UserDto
{
    public string Id { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public int Age => CalculateAge(DateOfBirth);
    public string AvatarUrl { get; init; } = string.Empty;

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
}

/// <summary>
/// DTO for creating a new user
/// </summary>
public record CreateUserDto
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string AvatarUrl { get; init; } = string.Empty;
}

/// <summary>
/// DTO for updating an existing user
/// </summary>
public record UpdateUserDto
{
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string AvatarUrl { get; init; } = string.Empty;
}
