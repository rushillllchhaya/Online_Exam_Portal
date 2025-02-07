using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SubmissionTableModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubmissionID { get; set; }
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string? Answer { get; set; }
        public decimal Grade { get; set; }
        public string? Feedback { get; set; }


        //navigation
        [ForeignKey("UserID")]
        public UsersModel User { get; set; }

        [ForeignKey("ExamID")]
        public ExamsModel Exam { get; set; }

        [ForeignKey("QuestionID")]


    }
}