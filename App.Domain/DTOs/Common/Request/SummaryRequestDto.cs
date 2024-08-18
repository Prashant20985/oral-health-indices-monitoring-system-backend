namespace App.Domain.DTOs.Common.Request;

public class SummaryRequestDto
{
    /// <summary>
    /// Gets or initializes the Patient Recommendations
    /// </summary>
    public string PatientRecommendations { get; init; }

    /// <summary>
    /// Gets or initializes the Need For Dental Interventions
    /// </summary>
    public string NeedForDentalInterventions { get; init; }

    /// <summary>
    /// Gets or initializes the Proposed Treatment
    /// </summary>
    public string ProposedTreatment { get; init; }

    /// <summary>
    /// Gets or initializes the Description
    /// </summary>
    public string Description { get; init; }
}
