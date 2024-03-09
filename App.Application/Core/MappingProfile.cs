using App.Domain.DTOs;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
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

        // CreateMap<TSource, TDestination> creates a mapping from Patient to ResearchGroupPatientDto
        CreateMap<Patient, ResearchGroupPatientDto>()
           .ForMember(x => x.AddedBy, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"));

        // CreateMap<TSource, TDestination> creates a mapping from ResearchGroup to ResearchGroupDto
        CreateMap<ResearchGroup, ResearchGroupDto>()
            .ForMember(x => x.CreatedBy, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(x => x.Patients, o => o.MapFrom(s => s.Patients));

        // CreateMap<TSource, TDestination> creates a mapping from Patient to PatientDto
        CreateMap<Patient, PatientDto>()
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(x => x.ResearchGroupName, o => o.MapFrom(s => s.ResearchGroup.GroupName));

        // CreateMap<TSource, TDestination> creates a mapping from Patient to PatientExaminationDto
        CreateMap<Patient, PatientExaminationDto>()
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(x => x.ResearchGroupName, o => o.MapFrom(s => s.ResearchGroup.GroupName))
            .ForMember(x => x.ExaminationCards, o => o.MapFrom(s => s.PatientExaminationCards));

        // CreateMap<TSource, TDestination> creates a mapping from RiskFactorAssessment to RiskFactorAssessmentDto
        CreateMap<RiskFactorAssessment, RiskFactorAssessmentDto>();

        // CreateMap<TSource, TDestination> creates a mapping from Bewe to BeweDto
        CreateMap<Bewe, BeweDto>();

        // CreateMap<TSource, TDestination> creates a mapping from APIBleeding to APIBleedingDto
        CreateMap<APIBleeding, APIBleedingDto>();

        // CreateMap<TSource, TDestination> creates a mapping from DMFT_DMFS to DMFT_DMFSDto
        CreateMap<DMFT_DMFS, DMFT_DMFSDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationResult to PatientExaminationResultDto
        CreateMap<PatientExaminationResult, PatientExaminationResultDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationCard to PatientExaminationCardDto
        CreateMap<PatientExaminationCard, PatientExaminationCardDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationRegularMode to PatientExaminationRegularModeDto
        CreateMap<PatientExaminationRegularMode, PatientExaminationRegularModeDto>()
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"));

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationTestMode to PatientExaminationTestModeDto
        CreateMap<PatientExaminationTestMode, PatientExaminationTestModeDto>()
            .ForMember(x => x.StudentName, o => o.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName}"))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"));

        // CreateMap<TSource, TDestination> creates a mapping from Exam to ExamDto
        CreateMap<Exam, ExamDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticePatient to PatientDto
        CreateMap<PracticePatient, PatientDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeRiskFactorAssessment to RiskFactorAssessment
        CreateMap<PracticeRiskFactorAssessment, RiskFactorAssessment>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeBewe to BeweDto
        CreateMap<PracticeBewe, BeweDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeDMFT_DMFS to DMFT_DMFSDto
        CreateMap<PracticeDMFT_DMFS, DMFT_DMFSDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeAPIBleeding to APIBleedingDto
        CreateMap<PracticeAPIBleeding, APIBleedingDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticePatientExaminationCard to PracticePatientExaminationCardDto
        CreateMap<PracticePatientExaminationCard, PracticePatientExaminationCardDto>()
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Exam.Group.Teacher.FirstName} {s.Exam.Group.Teacher.LastName} ({s.Exam.Group.Teacher.UserName})"))
            .ForMember(x => x.StudentName, o => o.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName} ({s.Student.UserName})"));
    }
}
