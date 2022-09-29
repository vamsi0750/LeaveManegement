using LeaveManegementApi.Models;
using LeaveManegementApi.Models.Movies;
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
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<Movie> Movies  { get; set; }

        //dotnet ef migrations add "migration name"   => to create migration
        //dotnet ef database update                   => to update database

    }
}
