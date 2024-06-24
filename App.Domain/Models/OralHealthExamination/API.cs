using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.OralHealthExamination;

public class API
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int APIResult { get; private set; }
    public int Maxilla { get; private set; }
    public int Mandible { get; private set; }
    public string Comment { get; private set; }
    public APIBleedingAssessmentModel AssessmentModel { get; private set; }
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comment = comment;

    public void SetAssessmentModel(APIBleedingAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    public void SetAPIResult(int apiResult) => APIResult = apiResult;

    public void CalculateAPIResult()
    {
        if (AssessmentModel is null) return;

        int GetCount(string[] quadrant, string value) => quadrant.Count(q => q.Equals(value));

        var quadrant1 = AssessmentModel.Quadrant1.ToArray();
        var quadrant2 = AssessmentModel.Quadrant2.ToArray();
        var quadrant3 = AssessmentModel.Quadrant3.ToArray();
        var quadrant4 = AssessmentModel.Quadrant4.ToArray();

        int quadrant1PlusCount = GetCount(quadrant1, "+");
        int quadrant2PlusCount = GetCount(quadrant2, "+");
        int quadrant3PlusCount = GetCount(quadrant3, "+");
        int quadrant4PlusCount = GetCount(quadrant4, "+");

        int quadrant1MinusCount = GetCount(quadrant1, "-");
        int quadrant2MinusCount = GetCount(quadrant2, "-");
        int quadrant3MinusCount = GetCount(quadrant3, "-");
        int quadrant4MinusCount = GetCount(quadrant4, "-");

        // Calculate API result
        int totalNumberOfSurfacesExamined = quadrant1PlusCount + quadrant2PlusCount
            + quadrant3PlusCount + quadrant4PlusCount
            + quadrant1MinusCount + quadrant2MinusCount
            + quadrant3MinusCount + quadrant4MinusCount;

        int totalNumberOfPlusCount = quadrant1PlusCount + quadrant2PlusCount
            + quadrant3PlusCount + quadrant4PlusCount;

        APIResult = (totalNumberOfPlusCount/totalNumberOfSurfacesExamined) * 100;

        // Calculate Maxilla
        int totalNumberOfSurfacesExaminedMaxilla = quadrant1PlusCount + quadrant2PlusCount
            + quadrant1MinusCount + quadrant2MinusCount;

        int totalNumberOfPlusCountMaxilla = quadrant1PlusCount + quadrant2PlusCount;

        Maxilla = (totalNumberOfPlusCountMaxilla/totalNumberOfSurfacesExaminedMaxilla) * 100;

        // Calculate Mandible
        int totalNumberOfSurfacesExaminedMandible = quadrant3PlusCount + quadrant4PlusCount
            + quadrant3MinusCount + quadrant4MinusCount;

        int totalNumberOfPlusCountMandible = quadrant3PlusCount + quadrant4PlusCount;

        Mandible = (totalNumberOfPlusCountMandible/totalNumberOfSurfacesExaminedMandible) * 100;
    }
}
