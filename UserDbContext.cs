using Microsoft.EntityFrameworkCore;
using TheJungleOfQuestsBackend.Entities;

namespace TheJungleOfQuestsBackend;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
}