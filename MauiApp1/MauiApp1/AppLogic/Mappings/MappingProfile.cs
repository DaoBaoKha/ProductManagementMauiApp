using AutoMapper;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.Core.Entities;

namespace MauiApp1.AppLogic.Mappings;

/// <summary>
/// AutoMapper profile for mapping between entities and DTOs
/// Add your mappings here
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
