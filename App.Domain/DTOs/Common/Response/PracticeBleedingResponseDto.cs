﻿using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Response;

public class PracticeBleedingResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the API and Bleeding assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the API (Assessment of Periodontal Inflammation) result.
    /// </summary>
    public int BleedingResult { get; init; }

    /// <summary>
    /// Gets or sets the Maxilla result.
    /// </summary>
    public int Maxilla { get; init; }

    /// <summary>
    /// Gets or sets the Mandible result.
    /// </summary>
    public int Mandible { get; init; }

    /// <summary>
    /// Gets or sets comment related to the API and Bleeding assessment.
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the Bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; init; }
}
