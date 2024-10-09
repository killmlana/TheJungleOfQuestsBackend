using Microsoft.EntityFrameworkCore;

namespace TheJungleOfQuestsBackend;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    //public DbSet<User> Users { get; set; }
}