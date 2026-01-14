using MauiApp1.AppLogic.DTOs;

namespace MauiApp1.AppLogic.Services.Interfaces;

/// <summary>
/// Example service interface for User operations
/// Define your business logic methods here
/// </summary>
public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(string id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task UpdateUserAsync(string id, UpdateUserDto dto);
    Task DeleteUserAsync(string id);
    Task<IEnumerable<UserDto>> SearchUsersByNameAsync(string searchTerm);
}
