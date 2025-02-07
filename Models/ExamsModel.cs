using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ExamsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }

        public int Title { get; set; }

        public string? Description { get; set; }

        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public int CreatedBy { get; set; }


        //navigation
        [ForeignKey("CreatedBy")]
        public ProfessorModel professorexam { get; set; }

        public List<QuestionModel> questionexam { get; set; }
    }

    // public class professorExam
    // {
    //     public int ProfessorID { get; set; }

    //     public int ExamID { get; set; }
    // }
}