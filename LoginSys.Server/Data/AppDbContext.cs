using Microsoft.EntityFrameworkCore;
using LoginSys.Server.Models;

    namespace LoginSys.Server.Data;
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { 

        }
         
        public DbSet<Users> Users { get; set; }
    }

