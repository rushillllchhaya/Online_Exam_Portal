using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models
{
    public class ProfessorModel
    {
        [Key]
        public int ProfessorID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }  // FK to UsersModel
        public UsersModel User { get; set; }

        public string? Designation { get; set; }
    }
}