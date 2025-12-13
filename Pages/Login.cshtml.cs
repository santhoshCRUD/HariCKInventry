using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HariCKInventry.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInput Input { get; set; } = new LoginInput();

        public string? Message { get; set; }

        // Hardcoded list of users (dummy data)
        private static readonly List<UserAccount> Users = new List<UserAccount>
        {
            new UserAccount { Username = "admin", Password = "admin@123" },
            new UserAccount { Username = "hari", Password = "123" },
            new UserAccount { Username = "karthick", Password = "123" },
            new UserAccount { Username = "santhosh", Password = "123" },
            new UserAccount { Username = "inmozhi", Password = "123" },
            new UserAccount { Username = "adi", Password = "123" },
            new UserAccount { Username = "baby", Password = "123" },
            new UserAccount { Username = "karthikk", Password = "123" }
        };

        public void OnGet()
        {
            // Initial load
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if user exists in the hardcoded list
            var user = Users.FirstOrDefault(u =>
                u.Username == Input.Username && u.Password == Input.Password);

            if (user != null)
            {
                // Store login status in session
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", user.Username);

                // Redirect to Categories page after successful login
                return RedirectToPage("/HomePage");
            }

            Message = "Invalid username or password.";
            return Page();
        }
    }

    public class LoginInput
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public class UserAccount
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}