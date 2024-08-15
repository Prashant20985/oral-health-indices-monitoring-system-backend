using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Data transfer object representing information about BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class PracticeBeweResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the BEWE assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the BEWE (Basic Erosive Wear Examination) result.
    /// </summary>
    public decimal BeweResult { get; init; }

    public decimal Sectant1 { get; init; }

    public decimal Sectant2 { get; init; }

    public decimal Sectant3 { get; init; }

    public decimal Sectant4 { get; init; }

    public decimal Sectant5 { get; init; }

    public decimal Sectant6 { get; init; }

    /// <summary>
    /// Gets or sets comments related to the BEWE assessment.
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the BEWE assessment.
    /// </summary>
    public BeweAssessmentModel AssessmentModel { get; init; }
}

