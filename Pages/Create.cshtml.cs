using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserCRUDExam.Models;

namespace UserCRUDExam.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _http;

        public CreateModel(HttpClient http)
        {
            _http = http;
        }

        [BindProperty]
        public User AddNewUser { get; set; }
        public List<User> Users { get; set; }
        public new User User { get; set; }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            var result = await _http.PostAsJsonAsync("https://localhost:7291/api/users", AddNewUser);

            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<User>();
                User = response;
                TempData["CreatedMessage"] = "User created successfully!";
            }

            return RedirectToPage("/index");
        }
    }
}
