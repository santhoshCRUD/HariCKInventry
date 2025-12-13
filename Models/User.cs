using System;
using System.ComponentModel.DataAnnotations;

namespace HariCKInventry.Models
{
    public class CreateUser
    {
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string RealName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int Status { get; set; } = 1;

        public int IsAdmin { get; set; } = 0;
    }
}