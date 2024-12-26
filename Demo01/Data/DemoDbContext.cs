using System;
using Demo01.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Data;

public class DemoDbContext : DbContext
{
    public DemoDbContext() : base()
    {

    }
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>().HasNoKey();
        modelBuilder.Entity<AuthenModel>().HasNoKey();
    }
    public DbSet<UserModel> User { get; set; }
    public DbSet<AuthenModel> Authens { get; set; }
}
