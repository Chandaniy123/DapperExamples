﻿using CQRsAndMEdiatorsEXample.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CQRsAndMEdiatorsEXample.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
