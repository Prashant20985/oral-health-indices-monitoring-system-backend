namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents the examination result for a patient in a practice scenario.
/// </summary>
public class PracticePatientExaminationResult(Guid beweId, Guid dMFT_DMFSId, Guid aPIId, Guid bleedingId)
{

    /// <summary>
    /// Gets or sets the unique identifier of the patient examination result.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the unique identifier of the BEWE assessment associated with this examination result.
    /// </summary>
    public Guid BeweId { get; set; } = beweId;

    /// <summary>
    /// Gets or sets the BEWE assessment associated with this examination result.
    /// </summary>
    public virtual PracticeBewe Bewe { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the DMFT_DMFS assessment associated with this examination result.
    /// </summary>
    public Guid DMFT_DMFSId { get; set; } = dMFT_DMFSId;

    /// <summary>
    /// Gets or sets the DMFT_DMFS assessment associated with this examination result.
    /// </summary>
    public virtual PracticeDMFT_DMFS DMFT_DMFS { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the API associated with this examination result.
    /// </summary>
    public Guid APIId { get; set; } = aPIId;

    /// <summary>
    /// Gets or sets the API associated with this examination result.
    /// </summary>
    public virtual PracticeAPI API { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the bleeding assessment associated with this examination result.
    /// </summary>
    public Guid BleedingId { get; set; } = bleedingId;

    /// <summary>
    /// Gets or sets the bleeding assessment associated with this examination result.
    /// </summary>
    public virtual PracticeBleeding Bleeding { get; set; }

    /// <summary>
    /// Gets or sets the examination card associated with this examination result.
    /// </summary>
    public virtual PracticePatientExaminationCard PracticePatientExaminationCard { get; set; }
}

