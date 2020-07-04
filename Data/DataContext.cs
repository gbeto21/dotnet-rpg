using dotnet_rpg.Model;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
        public DataContext(DbContextOptions<DataContext> pOptions): base(pOptions){}
    }
}