using AutoMapper;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.AppLogic.Services.Interfaces;
using MauiApp1.Core.Entities;
using MauiApp1.Core.Interfaces;

namespace MauiApp1.AppLogic.Services.Implementations;

/// <summary>
/// Example service implementation for User operations
/// Implements business logic and uses repositories for data access
/// </summary>
public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        // Add your business logic validation here
        var user = _mapper.Map<User>(dto);
        var created = await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(created);
    }

    public async Task UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found");
        }

        // Update only specified fields
        _mapper.Map(dto, existingUser);
        await _userRepository.UpdateAsync(id, existingUser);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserDto>> SearchUsersByNameAsync(string searchTerm)
    {
        var users = await _userRepository.FindAsync(u => 
            u.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            u.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
