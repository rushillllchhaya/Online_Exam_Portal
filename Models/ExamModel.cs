using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ExamModel
    {
        [Key]
        public int ExamID { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }
        public DateTime Schedule { get; set; }
        public int Duration { get; set; }  // Duration in minutes

        // Foreign Key for Subject
        public int SubjectID { get; set; }
        public SubjectModel Subject { get; set; }

        // Alias for Subject (not mapped to database)
        [NotMapped]
        public SubjectModel SubjectExam => Subject;

        // Foreign Key for Professor
        public int CreatedBy { get; set; }
        public ProfessorModel Professor { get; set; }

        // Navigation Properties
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
        public List<SubmissionModel> Submissions { get; set; } = new List<SubmissionModel>();
    }
}
