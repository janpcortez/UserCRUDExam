using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserCRUDExam.Models;

namespace UserCRUDExam.Pages
{
    public class UsersModel : PageModel
    {
        private readonly HttpClient _http;

        public UsersModel(HttpClient http)
        {
            _http = http;
        }

        [BindProperty]
        public User EditedUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditedUser = await GetUserAsync(id);
            return Page();
        }

        private async Task<User> GetUserAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<User>($"https://localhost:7291/api/users/{id}") ?? new User();
            return result;
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            var result = await _http.PutAsJsonAsync($"https://localhost:7291/api/users/{user.Id}", EditedUser);

            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<User>();
                EditedUser = response;
                TempData["SuccessMessage"] = "User updated successfully!";
            }

            return RedirectToPage("/index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _http.DeleteAsync($"https://localhost:7291/api/users/{id}");

            if (result.IsSuccessStatusCode)
            {
                TempData["DeletedMessage"] = "User deleted successfully";
            }
            else
            {
                TempData["DeletedError"] = "Failed to delete user";
            }

            return RedirectToPage("/index");
        }
    }
}
