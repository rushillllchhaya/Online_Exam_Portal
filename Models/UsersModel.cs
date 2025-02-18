using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class UsersModel : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string? Name { get; set; }

        public string? Role { get; set; }

        public string Password { get; set; }

        // Override Id property to use UserID
        public override int Id
        {
            get => UserID;
            set => UserID = value;
        }
    }
}
