using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class QuestionModel
    {
        public int QuestionID { get; set; }
        public int ExamID { get; set; }
        public ExamModel Exam { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Options { get; set; }
        public string CorrectAnswer { get; set; }
    }


    public class ExamQuestion
    {
        public int ExamID { get; set; }
        public int QuestionID { get; set; }
    }
}