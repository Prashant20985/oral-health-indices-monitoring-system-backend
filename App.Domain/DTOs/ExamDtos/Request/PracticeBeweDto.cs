using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.ExamDtos.Request;
/// <summary>
///  Represents the Practice BEWE DTO.
/// </summary>
public class PracticeBeweDto
{
    /// <summary>
    ///  Gets or sets the unique identifier for the BEWE assessment.
    /// </summary>
    public decimal BeweResult { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant1 result.
    /// </summary>
    public decimal Sectant1 { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant2 result.
    /// </summary>
    public decimal Sectant2 { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant3 result.
    /// </summary>
    public decimal Sectant3 { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant4 result.
    /// </summary>
    public decimal Sectant4 { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant5 result.
    /// </summary>
    public decimal Sectant5 { get; set; }

    /// <summary>
    ///  Gets or sets the Sectant6 result.
    /// </summary>
    public decimal Sectant6 { get; set; }

    /// <summary>
    ///  Gets or sets comments related to the BEWE assessment.
    /// </summary>
    public BeweAssessmentModel AssessmentModel { get; set; }
}
