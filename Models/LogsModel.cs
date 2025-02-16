using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LogsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        public int ExamID { get; set; }

        public int UserID { get; set; }

        public DateTime Timestamp { get; set; }

        public string? ActivityTime { get; set; }

        public string? Notes { get; set; }


        //navigation
        [ForeignKey("UserID")]
        public UsersModel User { get; set; }
    }
}