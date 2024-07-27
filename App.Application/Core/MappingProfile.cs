using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.DTOs.UserRequestDtos.Response;
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
        CreateMap<ApplicationUser, ApplicationUserResponseDto>()
            .ForMember(d => d.Role, o => o.MapFrom(s => s.ApplicationUserRoles
                .Select(x => x.ApplicationRole.Name)
                .FirstOrDefault()))
            .ForMember(d => d.UserType,
                o => o.MapFrom(s => s.GuestUserComment == null ? "RegularUser" : "GuestUser"));

        // CreateMap<TSource, TDestination> creates a mapping from CreateApplicationUserFromCsvDto to CreateApplicationUserDto
        CreateMap<CreateApplicationUserFromCsvRequestDto, CreateApplicationUserRequestDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.GetName(Role.Student)));

        // CreateMap<TSource, TDestination> creates a mapping from ApplicationUser to StudentDto
        CreateMap<ApplicationUser, StudentResponseDto>()
            .ForMember(x => x.Groups, o => o.MapFrom(s => s.StudentGroups
            .Select(x => x.Group.GroupName).ToList()));

        // CreateMap<TSource, TDestination> creates a mapping from UserRequest to UserRequestDto
        CreateMap<UserRequest, UserRequestResponseDto>()
            .ForMember(x => x.UserName, o => o.MapFrom(s => s.ApplicationUser.UserName));

        // CreateMap<TSource, TDestination> creates a mapping from Patient to ResearchGroupPatientDto
        CreateMap<Patient, ResearchGroupPatientResponseDto>()
            .ForMember(x => x.Gender, o => o.MapFrom(s => s.Gender.ToString()))
           .ForMember(x => x.AddedBy, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"));

        // CreateMap<TSource, TDestination> creates a mapping from ResearchGroup to ResearchGroupDto
        CreateMap<ResearchGroup, ResearchGroupResponseDto>()
            .ForMember(x => x.CreatedBy, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(x => x.Patients, o => o.MapFrom(s => s.Patients));

        // CreateMap<TSource, TDestination> creates a mapping from Patient to PatientDto
        CreateMap<Patient, PatientResponseDto>()
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName}"))
            .ForMember(x => x.ResearchGroupName, o => o.MapFrom(s => s.ResearchGroup.GroupName));

        // CreateMap<TSource, TDestination> creates a mapping from RiskFactorAssessment to RiskFactorAssessmentDto
        CreateMap<RiskFactorAssessment, RiskFactorAssessmentDto>();

        // CreateMap<TSource, TDestination> creates a mapping from Bewe to BeweDto
        CreateMap<Bewe, BeweResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from API to APIDto
        CreateMap<API, APIResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from Bleeding to BleedingDto
        CreateMap<Bleeding, BleedingResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from DMFT_DMFS to DMFT_DMFSDto
        CreateMap<DMFT_DMFS, DMFT_DMFSResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationResult to PatientExaminationResultDto
        CreateMap<PatientExaminationResult, PatientExaminationResultDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationCard to PatientExaminationCardDto
        CreateMap<PatientExaminationCard, PatientExaminationCardDto>()
            .ForMember(x => x.Summary, o => o.MapFrom(s => new SummaryResponseDto
            {
                Description = s.Description,
                NeedForDentalInterventions = s.NeedForDentalInterventions,
                PatientRecommendations = s.PatientRecommendations,
                ProposedTreatment = s.ProposedTreatment
            }))
            .ForMember(x => x.PatientName, o => o.MapFrom(s => $"{s.Patient.FirstName} {s.Patient.LastName}"))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName} ({s.Doctor.Email})"))
            .ForMember(x => x.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName} ({s.Student.Email})" : ""));

        // CreateMap<TSource, TDestination> creates a mapping from Exam to ExamDto
        CreateMap<Exam, ExamDto>()
            .ForMember(x => x.ExamStatus, o => o.MapFrom(s => Enum.GetName(s.ExamStatus)));

        // CreateMap<TSource, TDestination> creates a mapping from PracticePatient to PatientDto
        CreateMap<PracticePatient, PatientResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeRiskFactorAssessment to RiskFactorAssessment
        CreateMap<PracticeRiskFactorAssessment, RiskFactorAssessmentDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeBewe to BeweDto
        CreateMap<PracticeBewe, PracticeBeweResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeDMFT_DMFS to DMFT_DMFSDto
        CreateMap<PracticeDMFT_DMFS, PracticeDMFT_DMFSRespnseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeAPIBleeding to APIBleedingDto
        CreateMap<PracticeBleeding, PracticeBleedingResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticeAPI to APIDto
        CreateMap<PracticeAPI, PracticeAPIResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticePatientExaminationResult to PatientExaminationResultDto
        CreateMap<PracticePatientExaminationResult, PracticePatientExaminationResultResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from SummaryRequestDto to SummaryResponseDto
        CreateMap<SummaryRequestDto, SummaryResponseDto>();

        // CreateMap<TSource, TDestination> creates a mapping from PracticePatientExaminationCard to PracticePatientExaminationCardDto
        CreateMap<PracticePatientExaminationCard, PracticePatientExaminationCardDto>()
            .ForMember(x => x.Summary, o => o.MapFrom(s => new SummaryResponseDto
            {
                Description = s.Description,
                NeedForDentalInterventions = s.NeedForDentalInterventions,
                PatientRecommendations = s.PatientRecommendations,
                ProposedTreatment = s.ProposedTreatment
            }))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Exam.Group.Teacher.FirstName} {s.Exam.Group.Teacher.LastName} ({s.Exam.Group.Teacher.Email})"))
            .ForMember(x => x.StudentName, o => o.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName} ({s.Student.Email})"));

        // CreateMap<TSource, TDestination> creates a mapping from ApplicationUser to SupervisingDoctorResponseDto
        CreateMap<ApplicationUser, SupervisingDoctorResponseDto>()
            .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName} ({s.Email})"));

        // CreateMap<TSource, TDestination> creates a mapping from PatientExaminationCard to PatientDetailsWithExaminationCards
        CreateMap<PatientExaminationCard, PatientDetailsWithExaminationCards>()
            .ForMember(x => x.Patient, o => o.MapFrom(s => s.Patient))
            .ForMember(x => x.Summary, o => o.MapFrom(s => new SummaryResponseDto
            {
                Description = s.Description,
                NeedForDentalInterventions = s.NeedForDentalInterventions,
                PatientRecommendations = s.PatientRecommendations,
                ProposedTreatment = s.ProposedTreatment
            }))
            .ForMember(x => x.PatientName, o => o.MapFrom(s => $"{s.Patient.FirstName} {s.Patient.LastName}"))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.Doctor.FirstName} {s.Doctor.LastName} ({s.Doctor.Email})"))
            .ForMember(x => x.StudentName, o => o.MapFrom(s => $"{s.Student.FirstName} {s.Student.LastName} ({s.Student.Email})"));
    }
}
