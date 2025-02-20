using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UsersModel : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures it's the primary key
        public override int Id { get; set; }

        public int UserID
        {
            get => Id;
            set => Id = value;
        }

        public string Name { get; set; }
        public string Role { get; set; } // "Professor" or "Student"
        public string Password { get; set; }
    }
}
