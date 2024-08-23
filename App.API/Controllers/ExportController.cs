using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.Common.Tooth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace App.API.Controllers;
/// <summary>
///  Controller for handling exporting of examination data to Excel files.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Dentist_Teacher_Researcher, Dentist_Teacher_Examiner")]
public class ExportController : ControllerBase
{
    /// <summary>
    /// Exports the examination solution data to an Excel file, http post method.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("export-exam-solution")]
    public IActionResult ExportExamSolution([FromBody] PracticePatientExaminationCardDto data)
    {
        // Create a new Excel package
        using var package = new ExcelPackage();

        // Create a new worksheet for the details
        var detailsSheet = package.Workbook.Worksheets.Add("Details");

        // Add the examination information to the details sheet
        detailsSheet.Cells[1, 1].Value = "Exam Information";
        detailsSheet.Cells[1, 1, 1, 15].Merge = true;
        detailsSheet.Cells[1, 1].Style.Font.Bold = true;
        detailsSheet.Cells[1, 1].Style.Font.Size = 20;
        detailsSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        detailsSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        detailsSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        detailsSheet.Cells[3, 1].Value = "Student Name";
        detailsSheet.Cells[3, 1].Style.Font.Bold = true;
        detailsSheet.Cells[3, 2].Value = data.StudentName;

        detailsSheet.Cells[4, 1].Value = "Doctor Name";
        detailsSheet.Cells[4, 1].Style.Font.Bold = true;
        detailsSheet.Cells[4, 2].Value = data.DoctorName;

        detailsSheet.Cells[5, 1].Value = "Student Mark";
        detailsSheet.Cells[5, 1].Style.Font.Bold = true;
        detailsSheet.Cells[5, 2].Value = data.StudentMark;

        detailsSheet.Cells[6, 1].Value = "Doctor Comment";
        detailsSheet.Cells[6, 1].Style.Font.Bold = true;
        detailsSheet.Cells[6, 2].Value = data.DoctorComment;

        detailsSheet.Cells[9, 1].Value = "Patient Information";
        detailsSheet.Cells[9, 1, 9, 15].Merge = true;
        detailsSheet.Cells[9, 1].Style.Font.Bold = true;
        detailsSheet.Cells[9, 1].Style.Font.Size = 20;
        detailsSheet.Cells[9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        detailsSheet.Cells[9, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        detailsSheet.Cells[9, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        detailsSheet.Cells[11, 1].Value = "First Name";
        detailsSheet.Cells[11, 1].Style.Font.Bold = true;
        detailsSheet.Cells[11, 2].Value = data.PracticePatient.FirstName;

        detailsSheet.Cells[12, 1].Value = "Last Name";
        detailsSheet.Cells[12, 1].Style.Font.Bold = true;
        detailsSheet.Cells[12, 2].Value = data.PracticePatient.LastName;

        detailsSheet.Cells[13, 1].Value = "Email";
        detailsSheet.Cells[13, 1].Style.Font.Bold = true;
        detailsSheet.Cells[13, 2].Value = data.PracticePatient.Email;

        detailsSheet.Cells[14, 1].Value = "Gender";
        detailsSheet.Cells[14, 1].Style.Font.Bold = true;
        detailsSheet.Cells[14, 2].Value = data.PracticePatient.Gender;

        detailsSheet.Cells[15, 1].Value = "Ethnic Group";
        detailsSheet.Cells[15, 1].Style.Font.Bold = true;
        detailsSheet.Cells[15, 2].Value = data.PracticePatient.EthnicGroup;

        detailsSheet.Cells[16, 1].Value = "Other Group";
        detailsSheet.Cells[16, 1].Style.Font.Bold = true;
        detailsSheet.Cells[16, 2].Value = data.PracticePatient.OtherGroup;

        detailsSheet.Cells[17, 1].Value = "Years In School";
        detailsSheet.Cells[17, 1].Style.Font.Bold = true;
        detailsSheet.Cells[17, 2].Value = data.PracticePatient.YearsInSchool;

        detailsSheet.Cells[18, 1].Value = "Other Data";
        detailsSheet.Cells[18, 1].Style.Font.Bold = true;
        detailsSheet.Cells[18, 2].Value = data.PracticePatient.OtherData;

        detailsSheet.Cells[19, 1].Value = "Other Data 2";
        detailsSheet.Cells[19, 1].Style.Font.Bold = true;
        detailsSheet.Cells[19, 2].Value = data.PracticePatient.OtherData2;

        detailsSheet.Cells[20, 1].Value = "Other Data 3";
        detailsSheet.Cells[20, 1].Style.Font.Bold = true;
        detailsSheet.Cells[20, 2].Value = data.PracticePatient.OtherData3;

        detailsSheet.Cells[21, 1].Value = "Location";
        detailsSheet.Cells[21, 1].Style.Font.Bold = true;
        detailsSheet.Cells[21, 2].Value = data.PracticePatient.Location;

        detailsSheet.Cells[22, 1].Value = "Age";
        detailsSheet.Cells[22, 1].Style.Font.Bold = true;
        detailsSheet.Cells[22, 2].Value = data.PracticePatient.Age;

        // Create a new worksheet for the risk factor assessment
        var riskFactorAssessmentSheet = package.Workbook.Worksheets.Add("Risk Factor Assessment");
        AddRiskFactorAssessmentData(riskFactorAssessmentSheet, data.PracticeRiskFactorAssessment.RiskFactorAssessmentModel);

        // Create a new worksheet for the DMFT/DMFS data
        var dmft_dmfsSheet = package.Workbook.Worksheets.Add("DMFT/DMFS");
        dmft_dmfsSheet.Cells[1, 1].Value = "DMFT/DMFS";
        dmft_dmfsSheet.Cells[1, 1, 1, 15].Merge = true;
        dmft_dmfsSheet.Cells[1, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[1, 1].Style.Font.Size = 20;
        dmft_dmfsSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        dmft_dmfsSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        dmft_dmfsSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        dmft_dmfsSheet.Cells[3, 1].Value = "DMFT Result";
        dmft_dmfsSheet.Cells[3, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[3, 2].Value = data.PracticePatientExaminationResult.DMFT_DMFS.DMFTResult;

        dmft_dmfsSheet.Cells[4, 1].Value = "DMFS Result";
        dmft_dmfsSheet.Cells[4, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[4, 2].Value = data.PracticePatientExaminationResult.DMFT_DMFS.DMFSResult;

        dmft_dmfsSheet.Cells[5, 1].Value = "Prosthetic Status";
        dmft_dmfsSheet.Cells[5, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[5, 2].Value = data.PracticePatientExaminationResult.DMFT_DMFS.ProstheticStatus;

        dmft_dmfsSheet.Cells[6, 1].Value = "Comment";
        dmft_dmfsSheet.Cells[6, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[6, 2].Value = data.PracticePatientExaminationResult.DMFT_DMFS.Comment;

        // Add the DMFT/DMFS data to the worksheet
        AddDMFT_DMFSUpperMouthData(dmft_dmfsSheet, data.PracticePatientExaminationResult.DMFT_DMFS.AssessmentModel);
        AddDMFT_DMFSLowermouthData(dmft_dmfsSheet, data.PracticePatientExaminationResult.DMFT_DMFS.AssessmentModel);
        AddDMFT_DMFSExtraToothData(dmft_dmfsSheet, data.PracticePatientExaminationResult.DMFT_DMFS.AssessmentModel);

        // Create a new worksheet for the BEWE data
        var beweSheet = package.Workbook.Worksheets.Add("BEWE");

        // Add the BEWE data to the worksheet
        beweSheet.Cells[1, 1].Value = "BEWE";
        beweSheet.Cells[1, 1, 1, 15].Merge = true;
        beweSheet.Cells[1, 1].Style.Font.Bold = true;
        beweSheet.Cells[1, 1].Style.Font.Size = 20;
        beweSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        beweSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        beweSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        beweSheet.Cells[3, 1].Value = "BEWE Result";
        beweSheet.Cells[3, 1].Style.Font.Bold = true;
        beweSheet.Cells[3, 2].Value = data.PracticePatientExaminationResult.Bewe.BeweResult;

        beweSheet.Cells[4, 1].Value = "Comment";
        beweSheet.Cells[4, 1].Style.Font.Bold = true;
        beweSheet.Cells[4, 2].Value = data.PracticePatientExaminationResult.Bewe.Comment;

        // Add the BEWE data to the worksheet
        AddBeweData(beweSheet, data.PracticePatientExaminationResult.Bewe.AssessmentModel);

        // Create a new worksheet for the API data
        var apiSheet = package.Workbook.Worksheets.Add("API");

        // Add the API data to the worksheet
        apiSheet.Cells[1, 1].Value = "API";
        apiSheet.Cells[1, 1, 1, 15].Merge = true;
        apiSheet.Cells[1, 1].Style.Font.Bold = true;
        apiSheet.Cells[1, 1].Style.Font.Size = 20;
        apiSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        apiSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        apiSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        apiSheet.Cells[3, 1].Value = "API Result";
        apiSheet.Cells[3, 1].Style.Font.Bold = true;
        apiSheet.Cells[3, 2].Value = data.PracticePatientExaminationResult.API.APIResult;

        apiSheet.Cells[4, 1].Value = "Maxilla";
        apiSheet.Cells[4, 1].Style.Font.Bold = true;
        apiSheet.Cells[4, 2].Value = data.PracticePatientExaminationResult.API.Maxilla;

        apiSheet.Cells[5, 1].Value = "Mandible";
        apiSheet.Cells[5, 1].Style.Font.Bold = true;
        apiSheet.Cells[5, 2].Value = data.PracticePatientExaminationResult.API.Mandible;

        apiSheet.Cells[6, 1].Value = "Comment";
        apiSheet.Cells[6, 1].Style.Font.Bold = true;
        apiSheet.Cells[6, 2].Value = data.PracticePatientExaminationResult.API.Comment;

        // Add the API data to the worksheet
        AddAPIBleedingDate(apiSheet, data.PracticePatientExaminationResult.API.AssessmentModel);

        // Create a new worksheet for the bleeding data
        var bleedingSheet = package.Workbook.Worksheets.Add("Bleeding");

        // Add the bleeding data to the worksheet
        bleedingSheet.Cells[1, 1].Value = "Bleeding";
        bleedingSheet.Cells[1, 1, 1, 15].Merge = true;
        bleedingSheet.Cells[1, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[1, 1].Style.Font.Size = 20;
        bleedingSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        bleedingSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        bleedingSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        bleedingSheet.Cells[3, 1].Value = "Bleeding Result";
        bleedingSheet.Cells[3, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[3, 2].Value = data.PracticePatientExaminationResult.Bleeding.BleedingResult;

        bleedingSheet.Cells[4, 1].Value = "Maxilla";
        bleedingSheet.Cells[4, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[4, 2].Value = data.PracticePatientExaminationResult.Bleeding.Maxilla;

        bleedingSheet.Cells[5, 1].Value = "Mandible";
        bleedingSheet.Cells[5, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[5, 2].Value = data.PracticePatientExaminationResult.Bleeding.Mandible;

        bleedingSheet.Cells[6, 1].Value = "Comment";
        bleedingSheet.Cells[6, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[6, 2].Value = data.PracticePatientExaminationResult.Bleeding.Comment;

        // Add the bleeding data to the worksheet
        AddAPIBleedingDate(bleedingSheet, data.PracticePatientExaminationResult.Bleeding.AssessmentModel);

        // Create a new worksheet for the tooth data
        var summarySheet = package.Workbook.Worksheets.Add("Summary");

        // Add the summary data to the worksheet
        summarySheet.Cells[1, 1].Value = "Summary";
        summarySheet.Cells[1, 1, 1, 15].Merge = true;
        summarySheet.Cells[1, 1].Style.Font.Bold = true;
        summarySheet.Cells[1, 1].Style.Font.Size = 20;
        summarySheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        summarySheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        summarySheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        summarySheet.Cells[3, 1].Value = "Need For Dental Interventions";
        summarySheet.Cells[3, 1].Style.Font.Bold = true;
        summarySheet.Cells[3, 2].Value = data.Summary.NeedForDentalInterventions;

        summarySheet.Cells[4, 1].Value = "Proposed Treatment";
        summarySheet.Cells[4, 1].Style.Font.Bold = true;
        summarySheet.Cells[4, 2].Value = data.Summary.ProposedTreatment;
        summarySheet.Cells[4, 2, 4, 15].Merge = true;

        summarySheet.Cells[5, 1].Value = "Description";
        summarySheet.Cells[5, 1].Style.Font.Bold = true;
        summarySheet.Cells[5, 2].Value = data.Summary.Description;
        summarySheet.Cells[5, 2, 5, 15].Merge = true;

        summarySheet.Cells[6, 1].Value = "Patient Recommendations";
        summarySheet.Cells[6, 1].Style.Font.Bold = true;
        summarySheet.Cells[6, 2].Value = data.Summary.PatientRecommendations;
        summarySheet.Cells[6, 2, 6, 15].Merge = true;

        // Save the Excel package to a memory stream
        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        // Return the Excel file as a file result
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{data.StudentName}_ExamSolution.xlsx");
    }


    /// <summary>
    ///  Exports the examination card data to an Excel file, http post method.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("export-examination-card")]
    public IActionResult ExportExaminationCard([FromBody] PatientDetailsWithExaminationCards data)
    {
        // Create a new Excel package
        using var package = new ExcelPackage();

        // Create a new worksheet for the details
        var detailsSheet = package.Workbook.Worksheets.Add("Details");

        // Add the examination information to the details sheet
        detailsSheet.Cells[1, 1].Value = "Examination Information";
        detailsSheet.Cells[1, 1, 1, 15].Merge = true;
        detailsSheet.Cells[1, 1].Style.Font.Bold = true;
        detailsSheet.Cells[1, 1].Style.Font.Size = 20;
        detailsSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        detailsSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        detailsSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        detailsSheet.Cells[3, 1].Value = "Student Name";
        detailsSheet.Cells[3, 1].Style.Font.Bold = true;
        detailsSheet.Cells[3, 2].Value = data.StudentName ?? null;

        detailsSheet.Cells[4, 1].Value = "Doctor Name";
        detailsSheet.Cells[4, 1].Style.Font.Bold = true;
        detailsSheet.Cells[4, 2].Value = data.DoctorName;

        detailsSheet.Cells[5, 1].Value = "Total Score";
        detailsSheet.Cells[5, 1].Style.Font.Bold = true;
        detailsSheet.Cells[5, 2].Value = data.TotalScore;

        detailsSheet.Cells[6, 1].Value = "Doctor Comment";
        detailsSheet.Cells[6, 1].Style.Font.Bold = true;
        detailsSheet.Cells[6, 2].Value = data.DoctorComment;

        detailsSheet.Cells[7, 1].Value = "Student Comment";
        detailsSheet.Cells[7, 1].Style.Font.Bold = true;
        detailsSheet.Cells[7, 2].Value = data.StudentComment;

        detailsSheet.Cells[8, 1].Value = "Date Of Examination";
        detailsSheet.Cells[8, 1].Style.Font.Bold = true;
        detailsSheet.Cells[8, 2].Value = data.DateOfExamination.ToString();

        detailsSheet.Cells[9, 1].Value = "Is Regular Mode";
        detailsSheet.Cells[9, 1].Style.Font.Bold = true;
        detailsSheet.Cells[9, 2].Value = data.IsRegularMode;

        detailsSheet.Cells[12, 1].Value = "Patient Information";
        detailsSheet.Cells[12, 1, 12, 15].Merge = true;
        detailsSheet.Cells[12, 1].Style.Font.Bold = true;
        detailsSheet.Cells[12, 1].Style.Font.Size = 20;
        detailsSheet.Cells[12, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        detailsSheet.Cells[12, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        detailsSheet.Cells[12, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        detailsSheet.Cells[14, 1].Value = "First Name";
        detailsSheet.Cells[14, 1].Style.Font.Bold = true;
        detailsSheet.Cells[14, 2].Value = data.Patient.FirstName;

        detailsSheet.Cells[15, 1].Value = "Last Name";
        detailsSheet.Cells[15, 1].Style.Font.Bold = true;
        detailsSheet.Cells[15, 2].Value = data.Patient.LastName;

        detailsSheet.Cells[16, 1].Value = "Email";
        detailsSheet.Cells[16, 1].Style.Font.Bold = true;
        detailsSheet.Cells[16, 2].Value = data.Patient.Email;

        detailsSheet.Cells[17, 1].Value = "Gender";
        detailsSheet.Cells[17, 1].Style.Font.Bold = true;
        detailsSheet.Cells[17, 2].Value = data.Patient.Gender;

        detailsSheet.Cells[18, 1].Value = "Ethnic Group";
        detailsSheet.Cells[18, 1].Style.Font.Bold = true;
        detailsSheet.Cells[18, 2].Value = data.Patient.EthnicGroup;

        detailsSheet.Cells[19, 1].Value = "Other Group";
        detailsSheet.Cells[19, 1].Style.Font.Bold = true;
        detailsSheet.Cells[19, 2].Value = data.Patient.OtherGroup ?? null;

        detailsSheet.Cells[20, 1].Value = "Years In School";
        detailsSheet.Cells[20, 1].Style.Font.Bold = true;
        detailsSheet.Cells[20, 2].Value = data.Patient.YearsInSchool;

        detailsSheet.Cells[21, 1].Value = "Other Data";
        detailsSheet.Cells[21, 1].Style.Font.Bold = true;
        detailsSheet.Cells[21, 2].Value = data.Patient.OtherData ?? null;

        detailsSheet.Cells[22, 1].Value = "Other Data 2";
        detailsSheet.Cells[22, 1].Style.Font.Bold = true;
        detailsSheet.Cells[22, 2].Value = data.Patient.OtherData2 ?? null;

        detailsSheet.Cells[23, 1].Value = "Other Data 3";
        detailsSheet.Cells[23, 1].Style.Font.Bold = true;
        detailsSheet.Cells[23, 2].Value = data.Patient.OtherData3 ?? null;

        detailsSheet.Cells[24, 1].Value = "Location";
        detailsSheet.Cells[24, 1].Style.Font.Bold = true;
        detailsSheet.Cells[24, 2].Value = data.Patient.Location;

        detailsSheet.Cells[25, 1].Value = "Age";
        detailsSheet.Cells[25, 1].Style.Font.Bold = true;
        detailsSheet.Cells[25, 2].Value = data.Patient.Age;

        detailsSheet.Cells[26, 1].Value = "Created At";
        detailsSheet.Cells[26, 1].Style.Font.Bold = true;
        detailsSheet.Cells[26, 2].Value = data.Patient.CreatedAt;

        detailsSheet.Cells[27, 1].Value = "Archive Comment";
        detailsSheet.Cells[27, 1].Style.Font.Bold = true;
        detailsSheet.Cells[27, 2].Value = data.Patient.ArchiveComment;

        // Create a new worksheet for the risk factor assessment
        var riskFactorAssessmentSheet = package.Workbook.Worksheets.Add("Risk Factor Assessment");
        // Add the risk factor assessment data to the worksheet
        AddRiskFactorAssessmentData(riskFactorAssessmentSheet, data.RiskFactorAssessment.RiskFactorAssessmentModel);

        // Create a new worksheet for the DMFT/DMFS data
        var dmft_dmfsSheet = package.Workbook.Worksheets.Add("DMFT/DMFS");
        
        // Add the DMFT/DMFS data to the worksheet
        dmft_dmfsSheet.Cells[1, 1].Value = "DMFT/DMFS";
        dmft_dmfsSheet.Cells[1, 1, 1, 15].Merge = true;
        dmft_dmfsSheet.Cells[1, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[1, 1].Style.Font.Size = 20;
        dmft_dmfsSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        dmft_dmfsSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        dmft_dmfsSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        dmft_dmfsSheet.Cells[3, 1].Value = "DMFT Result";
        dmft_dmfsSheet.Cells[3, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[3, 2].Value = data.PatientExaminationResult.DMFT_DMFS.DMFTResult;

        dmft_dmfsSheet.Cells[4, 1].Value = "DMFS Result";
        dmft_dmfsSheet.Cells[4, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[4, 2].Value = data.PatientExaminationResult.DMFT_DMFS.DMFSResult;

        dmft_dmfsSheet.Cells[5, 1].Value = "Prosthetic Status";
        dmft_dmfsSheet.Cells[5, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[5, 2].Value = data.PatientExaminationResult.DMFT_DMFS.ProstheticStatus;

        dmft_dmfsSheet.Cells[6, 1].Value = "Doctor Comment";
        dmft_dmfsSheet.Cells[6, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[6, 2].Value = data.PatientExaminationResult.DMFT_DMFS.DoctorComment;

        dmft_dmfsSheet.Cells[7, 1].Value = "Student Comment";
        dmft_dmfsSheet.Cells[7, 1].Style.Font.Bold = true;
        dmft_dmfsSheet.Cells[7, 2].Value = data.PatientExaminationResult.DMFT_DMFS.StudentComment;

        // Add the DMFT/DMFS data to the worksheet
        AddDMFT_DMFSUpperMouthData(dmft_dmfsSheet, data.PatientExaminationResult.DMFT_DMFS.AssessmentModel);
        AddDMFT_DMFSLowermouthData(dmft_dmfsSheet, data.PatientExaminationResult.DMFT_DMFS.AssessmentModel);
        AddDMFT_DMFSExtraToothData(dmft_dmfsSheet, data.PatientExaminationResult.DMFT_DMFS.AssessmentModel);

        // Create a new worksheet for the BEWE data
        var beweSheet = package.Workbook.Worksheets.Add("BEWE");

        // Add the BEWE data to the worksheet
        beweSheet.Cells[1, 1].Value = "BEWE";
        beweSheet.Cells[1, 1, 1, 15].Merge = true;
        beweSheet.Cells[1, 1].Style.Font.Bold = true;
        beweSheet.Cells[1, 1].Style.Font.Size = 20;
        beweSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        beweSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        beweSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        beweSheet.Cells[3, 1].Value = "BEWE Result";
        beweSheet.Cells[3, 1].Style.Font.Bold = true;
        beweSheet.Cells[3, 2].Value = data.PatientExaminationResult.Bewe.BeweResult;

        beweSheet.Cells[4, 1].Value = "Doctor Comment";
        beweSheet.Cells[4, 1].Style.Font.Bold = true;
        beweSheet.Cells[4, 2].Value = data.PatientExaminationResult.Bewe.DoctorComment;

        beweSheet.Cells[5, 1].Value = "Student Comment";
        beweSheet.Cells[5, 1].Style.Font.Bold = true;
        beweSheet.Cells[5, 2].Value = data.PatientExaminationResult.Bewe.StudentComment;

        // Add the BEWE data to the worksheet
        AddBeweData(beweSheet, data.PatientExaminationResult.Bewe.AssessmentModel);

        // Create a new worksheet for the API data
        var apiSheet = package.Workbook.Worksheets.Add("API");

        //  
        apiSheet.Cells[1, 1].Value = "API";
        apiSheet.Cells[1, 1, 1, 15].Merge = true;
        apiSheet.Cells[1, 1].Style.Font.Bold = true;
        apiSheet.Cells[1, 1].Style.Font.Size = 20;
        apiSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        apiSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        apiSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        apiSheet.Cells[3, 1].Value = "API Result";
        apiSheet.Cells[3, 1].Style.Font.Bold = true;
        apiSheet.Cells[3, 2].Value = data.PatientExaminationResult.API.APIResult;

        apiSheet.Cells[4, 1].Value = "Maxilla";
        apiSheet.Cells[4, 1].Style.Font.Bold = true;
        apiSheet.Cells[4, 2].Value = data.PatientExaminationResult.API.Maxilla;

        apiSheet.Cells[5, 1].Value = "Mandible";
        apiSheet.Cells[5, 1].Style.Font.Bold = true;
        apiSheet.Cells[5, 2].Value = data.PatientExaminationResult.API.Mandible;

        apiSheet.Cells[6, 1].Value = "Doctor Comment";
        apiSheet.Cells[6, 1].Style.Font.Bold = true;
        apiSheet.Cells[6, 2].Value = data.PatientExaminationResult.API.DoctorComment;

        apiSheet.Cells[7, 1].Value = "Student Comment";
        apiSheet.Cells[7, 1].Style.Font.Bold = true;
        apiSheet.Cells[7, 2].Value = data.PatientExaminationResult.API.StudentComment;

        // Add the API data to the worksheet
        AddAPIBleedingDate(apiSheet, data.PatientExaminationResult.API.AssessmentModel);

        // Create a new worksheet for the bleeding data
        var bleedingSheet = package.Workbook.Worksheets.Add("Bleeding");

        // Add the bleeding data to the worksheet
        bleedingSheet.Cells[1, 1].Value = "Bleeding";
        bleedingSheet.Cells[1, 1, 1, 15].Merge = true;
        bleedingSheet.Cells[1, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[1, 1].Style.Font.Size = 20;
        bleedingSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        bleedingSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        bleedingSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        bleedingSheet.Cells[3, 1].Value = "Bleeding Result";
        bleedingSheet.Cells[3, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[3, 2].Value = data.PatientExaminationResult.Bleeding.BleedingResult;

        bleedingSheet.Cells[4, 1].Value = "Maxilla";
        bleedingSheet.Cells[4, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[4, 2].Value = data.PatientExaminationResult.Bleeding.Maxilla;

        bleedingSheet.Cells[5, 1].Value = "Mandible";
        bleedingSheet.Cells[5, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[5, 2].Value = data.PatientExaminationResult.Bleeding.Mandible;

        bleedingSheet.Cells[6, 1].Value = "Doctor Comment";
        bleedingSheet.Cells[6, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[6, 2].Value = data.PatientExaminationResult.Bleeding.DoctorComment;

        bleedingSheet.Cells[7, 1].Value = "Student Comment";
        bleedingSheet.Cells[7, 1].Style.Font.Bold = true;
        bleedingSheet.Cells[7, 2].Value = data.PatientExaminationResult.Bleeding.StudentComment;

        // Add the bleeding data to the worksheet
        AddAPIBleedingDate(bleedingSheet, data.PatientExaminationResult.Bleeding.AssessmentModel);

        // Create a new worksheet for the summary data
        var summarySheet = package.Workbook.Worksheets.Add("Summary");

        summarySheet.Cells[1, 1].Value = "Summary";
        summarySheet.Cells[1, 1, 1, 15].Merge = true;
        summarySheet.Cells[1, 1].Style.Font.Bold = true;
        summarySheet.Cells[1, 1].Style.Font.Size = 20;
        summarySheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        summarySheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        summarySheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        summarySheet.Cells[3, 1].Value = "Need For Dental Interventions";
        summarySheet.Cells[3, 1].Style.Font.Bold = true;
        summarySheet.Cells[3, 2].Value = data.Summary.NeedForDentalInterventions;

        summarySheet.Cells[4, 1].Value = "Proposed Treatment";
        summarySheet.Cells[4, 1].Style.Font.Bold = true;
        summarySheet.Cells[4, 2].Value = data.Summary.ProposedTreatment;
        summarySheet.Cells[4, 2, 4, 15].Merge = true;

        summarySheet.Cells[5, 1].Value = "Description";
        summarySheet.Cells[5, 1].Style.Font.Bold = true;
        summarySheet.Cells[5, 2].Value = data.Summary.Description;
        summarySheet.Cells[5, 2, 5, 15].Merge = true;

        summarySheet.Cells[6, 1].Value = "Patient Recommendations";
        summarySheet.Cells[6, 1].Style.Font.Bold = true;
        summarySheet.Cells[6, 2].Value = data.Summary.PatientRecommendations;
        summarySheet.Cells[6, 2, 6, 15].Merge = true;

        // Save the Excel package to a memory stream
        var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        // Return the Excel file as a file result
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{data.PatientName}_{data.DateOfExamination.ToLongDateString()}.xlsx");
    }


    /// <summary>
    ///  Adds the risk factor assessment data to the Excel worksheet.
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="riskFactorAssessment"></param>
    private void AddRiskFactorAssessmentData(ExcelWorksheet sheet, RiskFactorAssessmentModel riskFactorAssessment)
    {
        // Add the risk factor assessment data to the worksheet

        sheet.Cells[1, 1].Value = "Risk Factor Assessment";
        sheet.Cells[1, 1, 1, 15].Merge = true;
        sheet.Cells[1, 1].Style.Font.Bold = true;
        sheet.Cells[1, 1].Style.Font.Size = 20;
        sheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        sheet.Cells[3, 1].Value = "Question Text";
        sheet.Cells[3, 1].Style.Font.Bold = true;
        sheet.Cells[3, 2].Value = "Low Risk";
        sheet.Cells[3, 2].Style.Font.Bold = true;
        sheet.Cells[3, 3].Value = "Moderate Risk";
        sheet.Cells[3, 3].Style.Font.Bold = true;
        sheet.Cells[3, 4].Value = "High Risk";
        sheet.Cells[3, 4].Style.Font.Bold = true;

        // Add the questions and answers to the worksheet
        var row = 4;
        foreach (var item in riskFactorAssessment.Questions)
        {
            sheet.Cells[row, 1].Value = item.QuestionText;
            sheet.Cells[row, 2].Value = item.Answer.LowRisk;
            sheet.Cells[row, 3].Value = item.Answer.ModerateRisk;
            sheet.Cells[row, 4].Value = item.Answer.HighRisk;
            row++;
        }

        // Add color to the cells
        using (var range = sheet.Cells[3, 1, row - 1, 4])
        {
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
        }
    }
/// <summary>
///  Adds the DMFT/DMFS data for the lower mouth to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="data"></param>
    private void AddDMFT_DMFSUpperMouthData(ExcelWorksheet sheet, DMFT_DMFSAssessmentModel data)
    {
        // Add the DMFT/DMFS data for the upper mouth to the worksheet
        sheet.Cells[10, 1].Value = "Upper Mouth";
        sheet.Cells[10, 1].Style.Font.Bold = true;
        sheet.Cells[10, 1, 10, 33].Merge = true;
        sheet.Cells[10, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[10, 1].Style.Font.Size = 20;
        sheet.Cells[10, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[10, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        string[] upperMouthTeethWithO = ["Tooth 18", "Tooth 17", "Tooth 16", "Tooth 15", "Tooth 14", "Tooth 24", "Tooth 25", "Tooth 26", "Tooth 27", "Tooth 28"];

        // Add teeth headers
        string[] upperMouthTeethWithoutO = ["Tooth 13", "Tooth 12", "Tooth 11", "Tooth 21", "Tooth 22", "Tooth 23"];

        // Add teeth headers
        int column = 1;
        foreach (var tooth in upperMouthTeethWithO)
        {
            sheet.Cells[11, column].Value = tooth;
            sheet.Cells[11, column, 11, column + 1].Merge = true;
            sheet.Cells[11, column].Style.Font.Bold = true;
            sheet.Cells[11, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            column += 2;
        }

        // Add teeth headers
        foreach (var tooth in upperMouthTeethWithoutO)
        {
            sheet.Cells[11, column].Value = tooth;
            sheet.Cells[11, column, 11, column + 1].Merge = true;
            sheet.Cells[11, column].Style.Font.Bold = true;
            sheet.Cells[11, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            column += 2;
        }

        // Add color to the cells
        sheet.Cells[11, 1, 11, 33].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[11, 1, 11, 33].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Row header with assessments
        string[] assessmentsWithO = ["R", "B", "O", "L", "D", "M"];
        string[] assessmentsWithoutO = ["R", "B", "L", "D", "M", ""];

        // Fill assessments with "O" values
        for (int i = 0; i < assessmentsWithO.Length; i++)
        {
            sheet.Cells[12 + i, 1].Value = assessmentsWithO[i];
            sheet.Cells[12 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[12 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties with "O" values
        var upperMouth = data.UpperMouth;
        var teethPropertiesWithO = new[]
        {
            upperMouth.Tooth_18,
            upperMouth.Tooth_17,
            upperMouth.Tooth_16,
            upperMouth.Tooth_15,
            upperMouth.Tooth_14,
            upperMouth.Tooth_24,
            upperMouth.Tooth_25,
            upperMouth.Tooth_26,
            upperMouth.Tooth_27,
            upperMouth.Tooth_28
        };

      // Populate teeth properties with "O" values
        column = 2;
        for (int i = 0; i < teethPropertiesWithO.Length; i++)
        {
            var tooth = teethPropertiesWithO[i];

            sheet.Cells[12, column].Value = tooth.R;
            sheet.Cells[13, column].Value = tooth.B;
            sheet.Cells[14, column].Value = tooth.O;
            sheet.Cells[15, column].Value = tooth.L;
            sheet.Cells[16, column].Value = tooth.D;
            sheet.Cells[17, column].Value = tooth.M;

            // Add color to cells with "O" values
            using (var range = sheet.Cells[12, column, 17, column + 1])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }

        int columnWithoutO = 2 + teethPropertiesWithO.Length * 2;

        // Fill assessments without "O" values
        for (int i = 0; i < assessmentsWithoutO.Length; i++)
        {
            sheet.Cells[12 + i, columnWithoutO - 1].Value = assessmentsWithoutO[i];
            sheet.Cells[12 + i, columnWithoutO - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[12 + i, columnWithoutO - 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
        }

        // Populate teeth properties without "O" values
        var teethPropertiesWithoutO = new[]
        {
            upperMouth.Tooth_13,
            upperMouth.Tooth_12,
            upperMouth.Tooth_11,
            upperMouth.Tooth_21,
            upperMouth.Tooth_22,
            upperMouth.Tooth_23
        };

        // Populate teeth properties without "O" values
        for (int i = 0; i < teethPropertiesWithoutO.Length; i++)
        {
            var tooth = teethPropertiesWithoutO[i];

            sheet.Cells[12, columnWithoutO + i * 2].Value = tooth.R;
            sheet.Cells[13, columnWithoutO + i * 2].Value = tooth.B;
            sheet.Cells[14, columnWithoutO + i * 2].Value = tooth.L;
            sheet.Cells[15, columnWithoutO + i * 2].Value = tooth.D;
            sheet.Cells[16, columnWithoutO + i * 2].Value = tooth.M;

            // Add color to cells without "O" values
            using (var range = sheet.Cells[12, columnWithoutO + i * 2, 17, columnWithoutO + i * 2 + 1])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
            }
        }
    }

/// <summary>
///  Adds the DMFT/DMFS data for the lower mouth to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="data"></param>
    private void AddDMFT_DMFSLowermouthData(ExcelWorksheet sheet, DMFT_DMFSAssessmentModel data)
    {
        // Add the DMFT/DMFS data for the lower mouth to the worksheet
        sheet.Cells[20, 1].Value = "Lower Mouth";
        sheet.Cells[20, 1].Style.Font.Bold = true;
        sheet.Cells[20, 1, 20, 33].Merge = true;
        sheet.Cells[20, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[20, 1].Style.Font.Size = 20;
        sheet.Cells[20, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[20, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        string[] teethWithO = ["Tooth 48", "Tooth 47", "Tooth 46", "Tooth 45", "Tooth 44", "Tooth 34", "Tooth 35", "Tooth 36", "Tooth 37", "Tooth 38"];

        // Add teeth headers
        string[] teethWithoutO = ["Tooth 43", "Tooth 42", "Tooth 41", "Tooth 31", "Tooth 32", "Tooth 33"];

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[21, column].Value = tooth;
            sheet.Cells[21, column, 21, column + 1].Merge = true;
            sheet.Cells[21, column].Style.Font.Bold = true;
            sheet.Cells[21, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            column += 2;
        }

        // Add teeth headers
        foreach (var tooth in teethWithoutO)
        {
            sheet.Cells[21, column].Value = tooth;
            sheet.Cells[21, column, 21, column + 1].Merge = true;
            sheet.Cells[21, column].Style.Font.Bold = true;
            sheet.Cells[21, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            column += 2;
        }

        // Add color to the cells
        sheet.Cells[21, 1, 21, 33].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[21, 1, 21, 33].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Row header with assessments
        string[] assessmentsWithO = ["R", "B", "O", "L", "D", "M"];
        string[] assessmentsWithoutO = ["R", "B", "L", "D", "M", ""];

        // Fill assessments with "O" values
        for (int i = 0; i < assessmentsWithO.Length; i++)
        {
            sheet.Cells[22 + i, 1].Value = assessmentsWithO[i];
            sheet.Cells[22 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[22 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties with "O" values
        var lowerMouth = data.LowerMouth;

        var teethPropertiesWithO = new[]
        {
            lowerMouth.Tooth_48,
            lowerMouth.Tooth_47,
            lowerMouth.Tooth_46,
            lowerMouth.Tooth_45,
            lowerMouth.Tooth_44,
            lowerMouth.Tooth_34,
            lowerMouth.Tooth_35,
            lowerMouth.Tooth_36,
            lowerMouth.Tooth_37,
            lowerMouth.Tooth_38
        };

        // Populate teeth properties with "O" values
        column = 2;
        for (int i = 0; i < teethPropertiesWithO.Length; i++)
        {
            var tooth = teethPropertiesWithO[i];

            sheet.Cells[22, 2 + i * 2].Value = tooth.R;
            sheet.Cells[23, 2 + i * 2].Value = tooth.B;
            sheet.Cells[24, 2 + i * 2].Value = tooth.O;
            sheet.Cells[25, 2 + i * 2].Value = tooth.L;
            sheet.Cells[26, 2 + i * 2].Value = tooth.D;
            sheet.Cells[27, 2 + i * 2].Value = tooth.M;

            using (var range = sheet.Cells[22, column, 27, column + 1])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }

        // Populate teeth properties without "O" values
        int columnWithoutO = 2 + teethPropertiesWithO.Length * 2;
        for (int i = 0; i < assessmentsWithoutO.Length; i++)
        {
            sheet.Cells[22 + i, columnWithoutO - 1].Value = assessmentsWithoutO[i];
            sheet.Cells[22 + i, columnWithoutO - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[22 + i, columnWithoutO - 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
        }

        // Populate teeth properties without "O" values
        var teethPropertiesWithoutO = new[]
        {
            lowerMouth.Tooth_33,
            lowerMouth.Tooth_32,
            lowerMouth.Tooth_31,
            lowerMouth.Tooth_31,
            lowerMouth.Tooth_32,
            lowerMouth.Tooth_33
        };

        // Populate teeth properties without "O" values
        for (int i = 0; i < teethPropertiesWithoutO.Length; i++)
        {
            var tooth = teethPropertiesWithoutO[i];

            sheet.Cells[22, columnWithoutO + i * 2].Value = tooth.R;
            sheet.Cells[23, columnWithoutO + i * 2].Value = tooth.B;
            sheet.Cells[24, columnWithoutO + i * 2].Value = tooth.L;
            sheet.Cells[25, columnWithoutO + i * 2].Value = tooth.D;
            sheet.Cells[26, columnWithoutO + i * 2].Value = tooth.M;
            // Add color to cells without "O" values
            using (var range = sheet.Cells[22, columnWithoutO + i * 2, 27, columnWithoutO + i * 2 + 1])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
            }
        }
    }
/// <summary>
///  Adds the DMFT/DMFS data for the extra teeth to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="data"></param>
    private void AddDMFT_DMFSExtraToothData(ExcelWorksheet sheet, DMFT_DMFSAssessmentModel data)
    {
        // Add the DMFT/DMFS data for the extra teeth to the worksheet
        sheet.Cells[30, 1].Value = "Extra Tooth";
        sheet.Cells[30, 1, 30, 10].Merge = true;
        sheet.Cells[30, 1].Style.Font.Bold = true;
        sheet.Cells[30, 1].Style.Font.Size = 15;
        sheet.Cells[30, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[30, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[30, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the DMFT/DMFS data for the extra teeth to the worksheet
        sheet.Cells[31, 1].Value = "Tooth 55";
        sheet.Cells[31, 2].Value = "Tooth 54";
        sheet.Cells[31, 3].Value = "Tooth 53";
        sheet.Cells[31, 4].Value = "Tooth 52";
        sheet.Cells[31, 5].Value = "Tooth 51";
        sheet.Cells[31, 6].Value = "Tooth 61";
        sheet.Cells[31, 7].Value = "Tooth 62";
        sheet.Cells[31, 8].Value = "Tooth 63";
        sheet.Cells[31, 9].Value = "Tooth 64";
        sheet.Cells[31, 10].Value = "Tooth 65";

        // Add color to the cells
        sheet.Cells[31, 1, 31, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[31, 1, 31, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
        
        // Add the DMFT/DMFS data for the upper mouth to the worksheet
        sheet.Cells[32, 1].Value = data.UpperMouth.Tooth_55;
        sheet.Cells[32, 2].Value = data.UpperMouth.Tooth_54;
        sheet.Cells[32, 3].Value = data.UpperMouth.Tooth_53;
        sheet.Cells[32, 4].Value = data.UpperMouth.Tooth_52;
        sheet.Cells[32, 5].Value = data.UpperMouth.Tooth_51;
        sheet.Cells[32, 6].Value = data.UpperMouth.Tooth_61;
        sheet.Cells[32, 7].Value = data.UpperMouth.Tooth_62;
        sheet.Cells[32, 8].Value = data.UpperMouth.Tooth_63;
        sheet.Cells[32, 9].Value = data.UpperMouth.Tooth_64;
        sheet.Cells[32, 10].Value = data.UpperMouth.Tooth_65;

        // Add color to the cells
        sheet.Cells[32, 1, 32, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[32, 1, 32, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

        // Add the DMFT/DMFS data for the lower mouth to the worksheet
        sheet.Cells[33, 1].Value = "Tooth 85";
        sheet.Cells[33, 2].Value = "Tooth 84";
        sheet.Cells[33, 3].Value = "Tooth 83";
        sheet.Cells[33, 4].Value = "Tooth 82";
        sheet.Cells[33, 5].Value = "Tooth 81";
        sheet.Cells[33, 6].Value = "Tooth 71";
        sheet.Cells[33, 7].Value = "Tooth 72";
        sheet.Cells[33, 8].Value = "Tooth 73";
        sheet.Cells[33, 9].Value = "Tooth 74";
        sheet.Cells[33, 10].Value = "Tooth 75";

        // Add color to the cells
        sheet.Cells[33, 1, 33, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[33, 1, 33, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the DMFT/DMFS data for the lower mouth to the worksheet
        sheet.Cells[34, 1].Value = data.LowerMouth.Tooth_85;
        sheet.Cells[34, 2].Value = data.LowerMouth.Tooth_84;
        sheet.Cells[34, 3].Value = data.LowerMouth.Tooth_83;
        sheet.Cells[34, 4].Value = data.LowerMouth.Tooth_82;
        sheet.Cells[34, 5].Value = data.LowerMouth.Tooth_81;
        sheet.Cells[34, 6].Value = data.LowerMouth.Tooth_71;
        sheet.Cells[34, 7].Value = data.LowerMouth.Tooth_72;
        sheet.Cells[34, 8].Value = data.LowerMouth.Tooth_73;
        sheet.Cells[34, 9].Value = data.LowerMouth.Tooth_74;
        sheet.Cells[34, 10].Value = data.LowerMouth.Tooth_75;

        // Add color to the cells
        sheet.Cells[34, 1, 34, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[34, 1, 34, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
    }
/// <summary>
///  Adds the BEWE data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="bewe"></param>
    private void AddBeweData(ExcelWorksheet sheet, BeweAssessmentModel bewe)
    {
        // Add the BEWE data to the worksheet
        int startRow = 7;
        AddSectant1Data(sheet, bewe.Sectant1, startRow);
        startRow += 10;
        AddSectant2Data(sheet, bewe.Sectant2, startRow);
        startRow += 10;
        AddSectant3Data(sheet, bewe.Sectant3, startRow);
        startRow += 10;
        AddSectant4Data(sheet, bewe.Sectant4, startRow);
        startRow += 10;
        AddSectant5Data(sheet, bewe.Sectant5, startRow);
        startRow += 10;
        AddSectant6Data(sheet, bewe.Sectant6, startRow);
    }
/// <summary>
///  Adds the sectant 1 data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="sectant"></param>
/// <param name="startRow"></param>
    private void AddSectant1Data(ExcelWorksheet sheet, Sectant1 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 1";
        sheet.Cells[startRow, 1, startRow, 8].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        var teethWithO = new Dictionary<string, FiveSurfaceToothBEWE>
        {
            { "Tooth_17", sectant.Tooth_17 },
            { "Tooth_16", sectant.Tooth_16 },
            { "Tooth_15", sectant.Tooth_15 },
            { "Tooth_14", sectant.Tooth_14 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M", "O" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;
            sheet.Cells[startRow + 6, column].Value = tooth.Value.O;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 2, column - 1, startRow + 6, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }
    }
/// <summary>
///  Adds the sectant 2 data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="sectant"></param>
/// <param name="startRow"></param>

    private void AddSectant2Data(ExcelWorksheet sheet, Sectant2 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 2";
        sheet.Cells[startRow, 1, startRow, 12].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        var teethWithoutO = new Dictionary<string, FourSurfaceTooth>
        {
            { "Tooth_13", sectant.Tooth_13 },
            { "Tooth_12", sectant.Tooth_12 },
            { "Tooth_11", sectant.Tooth_11 },
            { "Tooth_21", sectant.Tooth_21 },
            { "Tooth_22", sectant.Tooth_22 },
            { "Tooth_23", sectant.Tooth_23 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithoutO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithoutO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 2, column - 1, startRow + 5, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }
    }

/// <summary>
///  Adds the sectant 3 data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="sectant"></param>
/// <param name="startRow"></param>
    private void AddSectant3Data(ExcelWorksheet sheet, Sectant3 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 3";
        sheet.Cells[startRow, 1, startRow, 8].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        var teethWithO = new Dictionary<string, FiveSurfaceToothBEWE>
        {
            { "Tooth_24", sectant.Tooth_24 },
            { "Tooth_25", sectant.Tooth_25 },
            { "Tooth_26", sectant.Tooth_26 },
            { "Tooth_27", sectant.Tooth_27 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M", "O" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;
            sheet.Cells[startRow + 6, column].Value = tooth.Value.O;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 2, column - 1, startRow + 6, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }
    }
/// <summary>
///  Adds the sectant 4 data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="sectant"></param>
/// <param name="startRow"></param>
    private void AddSectant4Data(ExcelWorksheet sheet, Sectant4 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 4";
        sheet.Cells[startRow, 1, startRow, 8].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        var teethWithO = new Dictionary<string, FiveSurfaceToothBEWE>
        {
            { "Tooth_34", sectant.Tooth_34 },
            { "Tooth_35", sectant.Tooth_35 },
            { "Tooth_36", sectant.Tooth_36 },
            { "Tooth_37", sectant.Tooth_37 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M", "O" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;
            sheet.Cells[startRow + 6, column].Value = tooth.Value.O;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 1, column - 1, startRow + 6, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }
    }
/// <summary>
///  Adds the sectant 5 data to the Excel worksheet.
/// </summary>
/// <param name="sheet"></param>
/// <param name="sectant"></param>
/// <param name="startRow"></param>
    private void AddSectant5Data(ExcelWorksheet sheet, Sectant5 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 5";
        sheet.Cells[startRow, 1, startRow, 12].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add teeth headers
        var teethWithoutO = new Dictionary<string, FourSurfaceTooth>
        {
            { "Tooth_33", sectant.Tooth_33 },
            { "Tooth_32", sectant.Tooth_32 },
            { "Tooth_31", sectant.Tooth_31 },
            { "Tooth_41", sectant.Tooth_41 },
            { "Tooth_42", sectant.Tooth_42 },
            { "Tooth_43", sectant.Tooth_43 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithoutO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithoutO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 1, column - 1, startRow + 5, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }
    }
    /// <summary>
    ///  Adds the sectant 6 data to the Excel worksheet.
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="sectant"></param>
    /// <param name="startRow"></param>
    private void AddSectant6Data(ExcelWorksheet sheet, Sectant6 sectant, int startRow)
    {
        // Add a title for the sectant
        sheet.Cells[startRow, 1].Value = "Sectant 6";
        sheet.Cells[startRow, 1, startRow, 8].Merge = true;
        sheet.Cells[startRow, 1].Style.Font.Bold = true;
        sheet.Cells[startRow, 1].Style.Font.Size = 15;
        sheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[startRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        var teethWithO = new Dictionary<string, FiveSurfaceToothBEWE>
        {
            { "Tooth_44", sectant.Tooth_44 },
            { "Tooth_45", sectant.Tooth_45 },
            { "Tooth_46", sectant.Tooth_46 },
            { "Tooth_47", sectant.Tooth_47 }
        };

        // Add teeth headers
        int column = 1;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 1, column].Value = tooth.Key;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Merge = true;
            sheet.Cells[startRow + 1, column].Style.Font.Bold = true;
            sheet.Cells[startRow + 1, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 1, column, startRow + 1, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            column += 2;
        }

        // Row header with BEWE scores
        string[] beweAssessments = { "B", "L", "D", "M", "O" };

        // Fill BEWE assessments
        for (int i = 0; i < beweAssessments.Length; i++)
        {
            sheet.Cells[startRow + 2 + i, 1].Value = beweAssessments[i];
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[startRow + 2 + i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
        }

        // Populate teeth properties
        column = 2;
        foreach (var tooth in teethWithO)
        {
            sheet.Cells[startRow + 2, column].Value = tooth.Value.B;
            sheet.Cells[startRow + 3, column].Value = tooth.Value.L;
            sheet.Cells[startRow + 4, column].Value = tooth.Value.D;
            sheet.Cells[startRow + 5, column].Value = tooth.Value.M;
            sheet.Cells[startRow + 6, column].Value = tooth.Value.O;

            // Add color to cells
            using (var range = sheet.Cells[startRow + 1, column - 1, startRow + 6, column])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
            }

            column += 2;
        }

    }

    /// <summary>
    ///  Adds the API Bleeding data to the Excel worksheet.
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="data"></param>
    private void AddAPIBleedingDate(ExcelWorksheet sheet, APIBleedingAssessmentModel data)
    {
        // Add the API Bleeding data to the worksheet
        sheet.Cells[9, 1].Value = "Quadrant 1";
        sheet.Cells[9, 1, 9, 7].Merge = true;
        sheet.Cells[9, 1].Style.Font.Bold = true;
        sheet.Cells[9, 1].Style.Font.Size = 15;
        sheet.Cells[9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[9, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[9, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 1 to the worksheet
        sheet.Cells[10, 1].Value = "q17";
        sheet.Cells[10, 2].Value = "q16";
        sheet.Cells[10, 3].Value = "q15";
        sheet.Cells[10, 4].Value = "q14";
        sheet.Cells[10, 5].Value = "q13";
        sheet.Cells[10, 6].Value = "q12";
        sheet.Cells[10, 7].Value = "q11";

        // Add color to the cells
        sheet.Cells[10, 1, 10, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[10, 1, 10, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 1 to the worksheet
        sheet.Cells[11, 1].Value = data.Quadrant1.Value7;
        sheet.Cells[11, 2].Value = data.Quadrant1.Value6;
        sheet.Cells[11, 3].Value = data.Quadrant1.Value5;
        sheet.Cells[11, 4].Value = data.Quadrant1.Value4;
        sheet.Cells[11, 5].Value = data.Quadrant1.Value3;
        sheet.Cells[11, 6].Value = data.Quadrant1.Value2;
        sheet.Cells[11, 7].Value = data.Quadrant1.Value1;

        // Add color to the cells
        sheet.Cells[11, 1, 11, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[11, 1, 11, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

        // Add the API Bleeding data for the Quadrant 2 to the worksheet
        sheet.Cells[9, 9].Value = "Quadrant 2";
        sheet.Cells[9, 9, 9, 15].Merge = true;
        sheet.Cells[9, 9].Style.Font.Bold = true;
        sheet.Cells[9, 9].Style.Font.Size = 15;
        sheet.Cells[9, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[9, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[9, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 2 to the worksheet
        sheet.Cells[10, 9].Value = "q21";
        sheet.Cells[10, 10].Value = "q22";
        sheet.Cells[10, 11].Value = "q23";
        sheet.Cells[10, 12].Value = "q24";
        sheet.Cells[10, 13].Value = "q25";
        sheet.Cells[10, 14].Value = "q26";
        sheet.Cells[10, 15].Value = "q27";

        // Add color to the cells
        sheet.Cells[10, 9, 10, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[10, 9, 10, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 2 to the worksheet
        sheet.Cells[11, 9].Value = data.Quadrant2.Value1;
        sheet.Cells[11, 10].Value = data.Quadrant2.Value2;
        sheet.Cells[11, 11].Value = data.Quadrant2.Value3;
        sheet.Cells[11, 12].Value = data.Quadrant2.Value4;
        sheet.Cells[11, 13].Value = data.Quadrant2.Value5;
        sheet.Cells[11, 14].Value = data.Quadrant2.Value6;
        sheet.Cells[11, 15].Value = data.Quadrant2.Value7;

        // Add color to the cells
        sheet.Cells[11, 9, 11, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[11, 9, 11, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

        // Add the API Bleeding data for the Quadrant 3 to the worksheet
        sheet.Cells[13, 1].Value = "Quadrant 3";
        sheet.Cells[13, 1, 13, 7].Merge = true;
        sheet.Cells[13, 1].Style.Font.Bold = true;
        sheet.Cells[13, 1].Style.Font.Size = 15;
        sheet.Cells[13, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[13, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[13, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 3 to the worksheet
        sheet.Cells[14, 1].Value = "q37";
        sheet.Cells[14, 2].Value = "q36";
        sheet.Cells[14, 3].Value = "q35";
        sheet.Cells[14, 4].Value = "q34";
        sheet.Cells[14, 5].Value = "q33";
        sheet.Cells[14, 6].Value = "q32";
        sheet.Cells[14, 7].Value = "q31";

        // Add color to the cells
        sheet.Cells[14, 1, 14, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[14, 1, 14, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 3 to the worksheet
        sheet.Cells[15, 1].Value = data.Quadrant3.Value7;
        sheet.Cells[15, 2].Value = data.Quadrant3.Value6;
        sheet.Cells[15, 3].Value = data.Quadrant3.Value5;
        sheet.Cells[15, 4].Value = data.Quadrant3.Value4;
        sheet.Cells[15, 5].Value = data.Quadrant3.Value3;
        sheet.Cells[15, 6].Value = data.Quadrant3.Value2;
        sheet.Cells[15, 7].Value = data.Quadrant3.Value1;

        // Add color to the cells
        sheet.Cells[15, 1, 15, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[15, 1, 15, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

        //  Add the API Bleeding data for the Quadrant 4 to the worksheet
        sheet.Cells[13, 9].Value = "Quadrant 4";
        sheet.Cells[13, 9, 13, 15].Merge = true;
        sheet.Cells[13, 9].Style.Font.Bold = true;
        sheet.Cells[13, 9].Style.Font.Size = 15;
        sheet.Cells[13, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[13, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[13, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 4 to the worksheet
        sheet.Cells[14, 9].Value = "q41";
        sheet.Cells[14, 10].Value = "q42";
        sheet.Cells[14, 11].Value = "q43";
        sheet.Cells[14, 12].Value = "q44";
        sheet.Cells[14, 13].Value = "q45";
        sheet.Cells[14, 14].Value = "q46";
        sheet.Cells[14, 15].Value = "q47";

        // Add color to the cells
        sheet.Cells[14, 9, 14, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[14, 9, 14, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

        // Add the API Bleeding data for the Quadrant 4 to the worksheet
        sheet.Cells[15, 9].Value = data.Quadrant4.Value1;
        sheet.Cells[15, 10].Value = data.Quadrant4.Value2;
        sheet.Cells[15, 11].Value = data.Quadrant4.Value3;
        sheet.Cells[15, 12].Value = data.Quadrant4.Value4;
        sheet.Cells[15, 13].Value = data.Quadrant4.Value5;
        sheet.Cells[15, 14].Value = data.Quadrant4.Value6;
        sheet.Cells[15, 15].Value = data.Quadrant4.Value7;

        //  Add color to the cells
        sheet.Cells[15, 9, 15, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[15, 9, 15, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
    }
}
