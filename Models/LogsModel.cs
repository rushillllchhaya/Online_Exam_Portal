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
        public int LogID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }  // FK to UsersModel
        public UsersModel User { get; set; }

        public DateTime Timestamp { get; set; }
        public string ActivityTime { get; set; }
        public string Notes { get; set; }
    }

}