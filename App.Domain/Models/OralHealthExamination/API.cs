using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.OralHealthExamination;
/// <summary>
/// Represents the API model.
/// </summary>
public class API
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    /// <summary>
    ///  Gets or sets the API result.
    /// </summary>
    public int APIResult { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Maxilla.
    /// </summary>
    public int Maxilla { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Mandible.
    /// </summary>
    public int Mandible { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Doctor comment.
    /// </summary>
    public string DoctorComment { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Student comment.
    /// </summary>
    public string StudentComment { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Assessment model.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Patient examination result.
    /// </summary>
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }
    
    /// <summary>
    ///  Method to add doctor comment.
    /// </summary>
    /// <param name="comment"></param>
    public void AddDoctorComment(string comment) => DoctorComment = comment;
    
    /// <summary>
    ///  Method to add student comment.
    /// </summary>
    /// <param name="comment"></param>
    public void AddStudentComment(string comment) => StudentComment = comment;

    /// <summary>
    ///  Method to set the assessment model.
    /// </summary>
    /// <param name="assessmentModel"></param>
    public void SetAssessmentModel(APIBleedingAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    /// <summary>
    ///  Method to set the patient examination result.
    /// </summary>
    /// <param name="apiResult"></param>
    public void SetAPIResult(int apiResult) => APIResult = apiResult;

    /// <summary>
    ///  Method to set the API result.
    /// </summary>
    /// <param name="maxilla"></param>
    public void SetMaxilla(int maxilla) => Maxilla = maxilla;

    /// <summary>
    ///  Method to set the mandible.
    /// </summary>
    /// <param name="mandible"></param>
    public void SetMandible(int mandible) => Mandible = mandible;

    /// <summary>
    ///  Calculates the API result.
    /// </summary>
    public void CalculateAPIResult()
    { 
        // Check if the assessment model is null
        if (AssessmentModel is null) return;

        // Get the count of the specified value in the quadrant
        int GetCount(string[] quadrant, string value) => quadrant.Count(q => q.Equals(value));

        // Get the quadrant arrays
        var quadrant1 = AssessmentModel.Quadrant1.ToArray();
        var quadrant2 = AssessmentModel.Quadrant2.ToArray();
        var quadrant3 = AssessmentModel.Quadrant3.ToArray();
        var quadrant4 = AssessmentModel.Quadrant4.ToArray();

        // Get the count of the specified value in each quadrant
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

        // Check if the total number of surfaces examined is 0
        if (totalNumberOfSurfacesExamined == 0)
        {
            SetAPIResult(0);
            SetMaxilla(0);
            SetMandible(0);
            return;
        }
        // else calculate the API result
        else
        {
            decimal apiResult = (decimal)totalNumberOfPlusCount / totalNumberOfSurfacesExamined * 100;
            SetAPIResult((int)Math.Round(apiResult));
        }

        // Calculate Maxilla
        int totalNumberOfSurfacesExaminedMaxilla = quadrant1PlusCount + quadrant2PlusCount
            + quadrant1MinusCount + quadrant2MinusCount;

        int totalNumberOfPlusCountMaxilla = quadrant1PlusCount + quadrant2PlusCount;

        // Check if the total number of surfaces examined in the maxilla is 0
        if (totalNumberOfSurfacesExaminedMaxilla == 0)
        {
            SetMaxilla(0);
        }
        else
        // else calculate the maxilla
        {
            decimal maxilla = (decimal)totalNumberOfPlusCountMaxilla / totalNumberOfSurfacesExaminedMaxilla * 100;
            SetMaxilla((int)Math.Round(maxilla));
        }


        // Calculate Mandible
        int totalNumberOfSurfacesExaminedMandible = quadrant3PlusCount + quadrant4PlusCount
            + quadrant3MinusCount + quadrant4MinusCount;

        int totalNumberOfPlusCountMandible = quadrant3PlusCount + quadrant4PlusCount;

        if (totalNumberOfSurfacesExaminedMandible == 0)
        {
            SetMandible(0);
        }
        else
        {
            decimal mandible = (decimal)totalNumberOfPlusCountMandible / totalNumberOfSurfacesExaminedMandible * 100;
            SetMandible((int)Math.Round(mandible));
        }
    }
}
