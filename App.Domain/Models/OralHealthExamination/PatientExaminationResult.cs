namespace App.Domain.Models.OralHealthExamination;

/// <summary>
/// Represents the result of a patient's oral health examination.
/// </summary>
public class PatientExaminationResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientExaminationResult"/> class.
    /// </summary>
    /// <param name="beweId">The ID of the BEWE (Basic Erosive Wear Examination) result.</param>
    /// <param name="dMFT_DMFSId">The ID of the DMFT/DMFS (Decayed, Missing, Filled Teeth/Surfaces) result.</param>
    /// <param name="aPIId">The ID of the API (Approximal Plaque Index) result.</param>
    /// <param name="bleedingId">The ID of the bleeding result.</param>
    public PatientExaminationResult(Guid beweId, Guid dMFT_DMFSId, Guid aPIId, Guid bleedingId)
    {
        Id = Guid.NewGuid();
        BeweId = beweId;
        DMFT_DMFSId = dMFT_DMFSId;
        APIId = aPIId;
        BleedingId = bleedingId;
    }

    /// <summary>
    /// Gets the unique identifier for the patient examination result.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the ID of the BEWE (Basic Erosive Wear Examination) result.
    /// </summary>
    public Guid BeweId { get; private set; }

    /// <summary>
    /// Gets or sets the BEWE (Basic Erosive Wear Examination) result.
    /// </summary>
    public virtual Bewe Bewe { get; set; }

    /// <summary>
    /// Gets the ID of the DMFT/DMFS (Decayed, Missing, Filled Teeth/Surfaces) result.
    /// </summary>
    public Guid DMFT_DMFSId { get; private set; }

    /// <summary>
    /// Gets or sets the DMFT/DMFS (Decayed, Missing, Filled Teeth/Surfaces) result.
    /// </summary>
    public virtual DMFT_DMFS DMFT_DMFS { get; set; }

    /// <summary>
    /// Gets the ID of the API (Approximal Plaque Index) result.
    /// </summary>
    public Guid APIId { get; private set; }

    /// <summary>
    /// Gets or sets the API (Approximal Plaque Index) result.
    /// </summary>
    public virtual API API { get; set; }

    /// <summary>
    /// Gets the ID of the bleeding result.
    /// </summary>
    public Guid BleedingId { get; private set; }

    /// <summary>
    /// Gets or sets the bleeding result.
    /// </summary>
    public virtual Bleeding Bleeding { get; set; }

    /// <summary>
    /// Gets or sets the patient examination card associated with the examination result.
    /// </summary>
    public virtual PatientExaminationCard PatientExaminationCard { get; set; }
}