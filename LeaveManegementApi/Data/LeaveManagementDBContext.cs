using LeaveManegementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManegementApi.Data
{
    public class LeaveManagementDBContext : DbContext
    {
        public LeaveManagementDBContext(DbContextOptions<LeaveManagementDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<SendGridTemplates> SendGridTemplates { get; set; }

    }
}
