using Microsoft.EntityFrameworkCore;
using UserCRUDExam.Models;

namespace UserCRUDExam.Data
{
    public class UsersDbContext : DbContext

    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
