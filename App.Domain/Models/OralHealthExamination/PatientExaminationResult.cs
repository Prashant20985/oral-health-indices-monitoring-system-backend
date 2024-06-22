namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationResult
{
    public PatientExaminationResult(Guid beweId, Guid dMFT_DMFSId, Guid aPIBleedingId)
    {
        Id = Guid.NewGuid();
        BeweId = beweId;
        DMFT_DMFSId = dMFT_DMFSId;
        APIBleedingId = aPIBleedingId;
    }

    public Guid Id { get; set; }

    public Guid BeweId { get; set; }
    public virtual Bewe Bewe { get; set; }

    public Guid DMFT_DMFSId { get; set; }
    public virtual DMFT_DMFS DMFT_DMFS { get; set; }

    public Guid APIBleedingId { get; set; }
    public virtual APIBleeding APIBleeding { get; set; }

    public virtual PatientExaminationCard PatientExaminationCard { get; set; }
}
