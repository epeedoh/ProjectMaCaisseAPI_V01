//using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectMaCaisseAPI_V01.Data;

   public class DataContext: IdentityDbContext<User>
    {
     

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Personne> Personnes { get; set; }
        public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
               .HasKey(u => u.UserID);

        modelBuilder.Entity<User>()
            .Property(u => u.UserID)
            .ValueGeneratedOnAdd(); // Définit que la valeur sera générée lors de l'ajout (auto-incrémentation)

        base.OnModelCreating(modelBuilder);
        SeedRoles(modelBuilder);
    }

    private void SeedRoles(ModelBuilder builder) 
    {
        builder.Entity<IdentityRole>().HasData
            (
             new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
             new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );
    }

}

