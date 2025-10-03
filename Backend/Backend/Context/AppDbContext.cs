using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context;

public class AppDbContext :DbContext
{
    //public DbSet<User> Users { get; set; }
  

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //User entity configuration
        /*
        modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        */
        
    }
}