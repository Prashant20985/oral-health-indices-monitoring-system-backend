﻿using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.ExamDtos.Request;
/// <summary>
///  Represents the Practice Bleeding DTO.
/// </summary>
public class PracticeBleedingDto
{
    /// <summary>
    ///  Gets or sets the unique identifier for the API and Bleeding assessment.
    /// </summary>
    public int BleedingResult { get; set; }
    
    /// <summary>
    ///   Gets or sets the Maxilla result.
    /// </summary>
    public int Maxilla { get; set; }
    
    /// <summary>
    ///  Gets or sets the Mandible result.
    /// </summary>
    public int Mandible { get; set; }
    
    /// <summary>
    ///  Gets or sets comment related to the API and Bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; set; }
}
