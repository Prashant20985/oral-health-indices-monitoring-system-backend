﻿using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create Bleeding Regular Mode Request DTO
/// </summary>
public class CreateBleedingRegularModeRequestDto
{
    /// <summary>
    /// Gets or initializes the comment
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes Bledding Assessment model
    /// </summary>
    public APIBleedingAssessmentModel BleedingAssessmentModel { get; init; }
}
