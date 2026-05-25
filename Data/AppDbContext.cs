using Microsoft.EntityFrameworkCore;
using VeyraApi.Models;

namespace VeyraApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<FinishingPackage> FinishingPackages => Set<FinishingPackage>();
    public DbSet<SmartDevice> SmartDevices => Set<SmartDevice>();
    public DbSet<SmartPackage> SmartPackages => Set<SmartPackage>();
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Gallery).HasColumnType("jsonb");
            entity.Property(e => e.Highlights).HasColumnType("jsonb");
            entity.HasMany(e => e.Units).WithOne().HasForeignKey(u => u.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("units");
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.ToTable("leads");
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<FinishingPackage>(entity =>
        {
            entity.ToTable("finishing_packages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Features).HasColumnType("jsonb");
        });

        modelBuilder.Entity<SmartDevice>(entity =>
        {
            entity.ToTable("smart_devices");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Benefits).HasColumnType("jsonb");
        });

        modelBuilder.Entity<SmartPackage>(entity =>
        {
            entity.ToTable("smart_packages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Devices).HasColumnType("jsonb");
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
