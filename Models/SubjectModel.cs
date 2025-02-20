using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SubjectModel
    {
        [Key]
        public int SubjectID { get; set; }

        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }

        [ForeignKey("Professor")]
        public int ProfessorID { get; set; }  // FK to Professor
        public ProfessorModel? Professor { get; set; }
        public List<ExamModel>? Exams { get; set; }
    }
}