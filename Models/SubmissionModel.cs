using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SubmissionModel
    {
        public int SubmissionID { get; set; }
        public int ExamID { get; set; }
        public ExamModel Exam { get; set; }
        public int StudentID { get; set; }
        public StudentModel Student { get; set; }
        public int QuestionID { get; set; }
        public QuestionModel QuestionSubmit { get; set; }
        public string Answer { get; set; }
    }

}