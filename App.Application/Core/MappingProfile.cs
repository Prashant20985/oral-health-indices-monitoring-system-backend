using App.Domain.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using AutoMapper;

namespace App.Application.Core;

/// <summary>
/// AutoMapper mapping configuration profile.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<TSource, TDestination> creates a mapping from ApplicationUser to ApplicationUserDto
        CreateMap<ApplicationUser, ApplicationUserDto>()
            .ForMember(d => d.Role, o => o.MapFrom(s => s.ApplicationUserRoles
                .Select(x => x.ApplicationRole.Name)
                .FirstOrDefault()))
            .ForMember(d => d.UserType,
                o => o.MapFrom(s => s.GuestUserComment == null ? "RegularUser" : "GuestUser"));

        // CreateMap<TSource, TDestination> creates a mapping from CreateApplicationUserFromCsvDto to CreateApplicationUserDto
        CreateMap<CreateApplicationUserFromCsvDto, CreateApplicationUserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.GetName(Role.Student)));

        // CreateMap<TSource, TDestination> creates a mapping from ApplicationUser to StudentDto
        CreateMap<ApplicationUser, StudentDto>()
            .ForMember(x => x.Groups, o => o.MapFrom(s => s.StudentGroups
            .Select(x => x.Group.GroupName).ToList()));

        // CreateMap<TSource, TDestination> creates a mapping from UserRequest to UserRequestDto
        CreateMap<UserRequest, UserRequestDto>()
            .ForMember(x => x.UserName, o => o.MapFrom(s => s.ApplicationUser.UserName));
    }
}
