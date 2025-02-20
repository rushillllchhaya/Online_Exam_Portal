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
        [Key]
        public int SubmissionID { get; set; }

        [ForeignKey("Exam")]
        public int ExamID { get; set; }  // FK to Exam
        public ExamModel Exam { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }  // FK to Student
        public StudentModel Student { get; set; }

        [ForeignKey("Question")]
        public int? QuestionID { get; set; }  // FK to Question (nullable to prevent cycle issues)
        public QuestionModel? Question { get; set; }

        public string Answer { get; set; }
    }


}