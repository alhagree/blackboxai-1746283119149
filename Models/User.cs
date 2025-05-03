using System.ComponentModel.DataAnnotations;

namespace WindowsApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public string FullName { get; set; }
        
        public string Email { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;
    }
}
