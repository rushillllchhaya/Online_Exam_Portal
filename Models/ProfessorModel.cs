using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace API.Models
{
    public class ProfessorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfessorID { get; set; }

        public int UserID { get; set; }

        public string? Name { get; set; }

        public int Number { get; set; }

        public string? Address { get; set; }

        public string? Designation { get; set; }

        // Navigation property for one-to-many relationship
        public List<SubjectModel>? Subjects { get; set; }

        [ForeignKey("UserID")]
        public required UsersModel User { get; set; }
    }


}