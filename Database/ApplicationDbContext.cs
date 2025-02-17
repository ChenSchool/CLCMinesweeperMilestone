using Microsoft.EntityFrameworkCore;
using CLCMinesweeperMilestone.Models;

namespace CLCMinesweeperMilestone.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
