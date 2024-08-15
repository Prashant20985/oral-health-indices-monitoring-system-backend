﻿using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.ExamDtos.Request;

public class PracticeBeweDto
{
    public decimal BeweResult { get; set; }

    public decimal Sectant1 { get; set; }

    public decimal Sectant2 { get; set; }

    public decimal Sectant3 { get; set; }

    public decimal Sectant4 { get; set; }

    public decimal Sectant5 { get; set; }

    public decimal Sectant6 { get; set; }

    public BeweAssessmentModel AssessmentModel { get; set; }
}
