using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using UserCRUDExam.Models;

namespace UserCRUDExam.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _http;

        public IndexModel(ILogger<IndexModel> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
        }

        public List<User> Users { get; set; }

        public async Task<List<User>> GetDataAsync()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("https://localhost:7291/api/users") ?? new List<User>();

            return result;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await GetDataAsync();
            return Page();
        }




    }
}