using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using Users.API.Models;

namespace UserCRUDExam.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<User> Users { get; private set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("MyWebApi");
            var response = await client.GetAsync("api/users");
            response.EnsureSuccessStatusCode();
            Users = await response.Content.ReadFromJsonAsync<List<User>>();
        }
    }
}
