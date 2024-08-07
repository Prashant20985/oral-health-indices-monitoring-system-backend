﻿using App.Domain.DTOs.Common.Request;
using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByStudent;

/// <summary>
/// Input parameters for creating patient examination card in regular mode
/// </summary>
/// <param name="PatientExaminationCardComment">The patient examination card comment</param>
/// <param name="AssignedDoctorId">The assigned doctor id</param>
/// <param name="RiskFactorAssessmentModel">The risk factor assessment model</param>
/// <param name="DMFT_DMFS">The create DMFT/DMFS request</param>
/// <param name="Bewe">The create BEWE request</param>
/// <param name="API">The create API request</param>
/// <param name="Bleeding">The create bleeding request</param>
public record CreatePatientExaminationCardByStudentInputParams(
    string AssignedDoctorId,
    string PatientExaminationCardComment,
    SummaryRequestDto Summary,
    RiskFactorAssessmentModel RiskFactorAssessmentModel,
    CreateDMFT_DMFSRequestDto DMFT_DMFS,
    CreateBeweRequestDto Bewe,
    CreateAPIRequestDto API,
    CreateBleedingRequestDto Bleeding);
