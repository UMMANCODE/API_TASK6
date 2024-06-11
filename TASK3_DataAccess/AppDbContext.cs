using System;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TASK3_Core.Entities;

namespace TASK3_DataAccess {
  public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
      builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
  }
}

