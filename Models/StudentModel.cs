using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class StudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public int UserID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public long Number { get; set; }
        public string? Address { get; set; }
        public string? SecondaryEmail { get; set; }


        //navigation
        [ForeignKey("UserID")]
        public UsersModel User { get; set; }
        public List<SubjectModel>? Studentsubject { get; set; }
    }

}