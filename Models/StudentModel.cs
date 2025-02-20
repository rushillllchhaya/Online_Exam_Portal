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
        public int StudentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }  // FK to UsersModel
        public UsersModel User { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}