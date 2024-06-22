namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationResult(Guid beweId, Guid dMFT_DMFSId, Guid aPIId, Guid bleedingId)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BeweId { get; set; } = beweId;
    public virtual Bewe Bewe { get; set; }

    public Guid DMFT_DMFSId { get; set; } = dMFT_DMFSId;
    public virtual DMFT_DMFS DMFT_DMFS { get; set; }

    public Guid APIId { get; set; } = aPIId;
    public virtual API API { get; set; }

    public Guid BleedingId { get; set; } = bleedingId;
    public virtual Bleeding Bleeding { get; set; }

    public virtual PatientExaminationCard PatientExaminationCard { get; set; }
}
