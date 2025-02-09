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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectID { get; set; }

        public string? SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public int ProfessorID { get; set; }


        //navigation
        [ForeignKey("ProfessorID")]
        public ProfessorModel? SubjectProfessor { get; set; }

        public List<ExamModel>? Exams { get; set; }
    }


    public class Subjectprofessor
    {
        public int ProfessorID { get; set; }

        public int SubjectID { get; set; }
    }
}