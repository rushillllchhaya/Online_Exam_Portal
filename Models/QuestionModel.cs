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
        [Key]
        public int QuestionID { get; set; }

        public string QuestionText { get; set; }
        public string QuestionType { get; set; }  // MCQ, Short Answer, etc.
        public string Options { get; set; }  // Comma-separated options for MCQs
        public string CorrectAnswer { get; set; }

        [ForeignKey("Exam")]
        public int ExamID { get; set; }  // FK to Exam
        public ExamModel Exam { get; set; }
    }

}