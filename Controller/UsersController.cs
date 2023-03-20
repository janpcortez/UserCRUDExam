using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCRUDExam.Data;
using UserCRUDExam.Models;

namespace UserCRUDExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersDbContext _usersDbContext;

        public UsersController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _usersDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {

            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found");

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Address = addUserRequest.Address,
                EmailAddress = addUserRequest.EmailAddress,
                ContactNumber = addUserRequest.ContactNumber
            };

            await _usersDbContext.Users.AddAsync(user);
            await _usersDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUserRequest, int id)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                user.FirstName = updateUserRequest.FirstName;
                user.LastName = updateUserRequest.LastName;
                user.Address = updateUserRequest.Address;
                user.EmailAddress = updateUserRequest.EmailAddress;
                user.ContactNumber = updateUserRequest.ContactNumber;

                await _usersDbContext.SaveChangesAsync();

                return Ok(user);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                _usersDbContext.Remove(user);
                await _usersDbContext.SaveChangesAsync();
                return Ok(user);
            }

            return NotFound();
        }


    }
}
