using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ExamModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }

        public int SubjectID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime Schedule { get; set; }
        public int Duration { get; set; }
        public int CreatedBy { get; set; }


        //navigation
        [ForeignKey("SubjectID")]
        public SubjectModel? SubjectExam { get; set; }

        [ForeignKey("CreatedBy")]
        public ProfessorModel? CreatedByProfessor { get; set; }

        public List<QuestionModel> Questions { get; set; }

        public List<SubmissionModel> Submissions { get; set; }
    }
}