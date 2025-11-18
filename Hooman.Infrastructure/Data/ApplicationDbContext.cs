
using Hooman.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hooman.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
    
  }

  // Identity Tables
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

      // Module Tables
    public DbSet<Module> Modules { get; set; }
    public DbSet<RoleModule> RoleModules { get; set; }
    public DbSet<UserModule> UserModules { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from separate files
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Or define inline configurations:
        ConfigureUser(modelBuilder);
        ConfigureRole(modelBuilder);
        ConfigureUserRole(modelBuilder);
        ConfigurePermission(modelBuilder);
        ConfigureRolePermission(modelBuilder);
        ConfigureUserPermission(modelBuilder);
        ConfigureRefreshToken(modelBuilder);
        ConfigureAuditLog(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);
            
            entity.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasMaxLength(512);
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);
            
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(500);
            
            entity.Property(e => e.EmailVerificationToken)
                .HasMaxLength(256);
            
            entity.Property(e => e.PasswordResetToken)
                .HasMaxLength(256);

            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.HasIndex(e => e.UserName)
                .IsUnique();
            
            entity.HasIndex(e => e.IsActive);

            entity.Ignore(e => e.FullName);
        });
    }

    private void ConfigureRole(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.NormalizedName)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.HasIndex(e => e.Name)
                .IsUnique();
            
            entity.HasIndex(e => e.NormalizedName)
                .IsUnique();
        });
    }

    private void ConfigureUserRole(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.RoleId);
            entity.HasIndex(e => new { e.UserId, e.RoleId })
                .IsUnique();
        });
    }

    private void ConfigurePermission(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permissions");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.NormalizedName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.HasIndex(e => e.Name)
                .IsUnique();
            
            entity.HasIndex(e => e.NormalizedName)
                .IsUnique();
        });
    }

    private void ConfigureRolePermission(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermissions");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.RoleId);
            entity.HasIndex(e => e.PermissionId);
            entity.HasIndex(e => new { e.RoleId, e.PermissionId })
                .IsUnique();
        });
    }

    private void ConfigureUserPermission(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.ToTable("UserPermissions");
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => new { e.UserId, e.PermissionId })
                .IsUnique();
        });
    }

    private void ConfigureRefreshToken(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(512);
            
            entity.Property(e => e.JwtId)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.RevokedById)
                .HasMaxLength(50);
            
            entity.Property(e => e.ReplacedByToken)
                .HasMaxLength(512);
            
            entity.Property(e => e.CreatedById)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(e => e.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Token)
                .IsUnique();
            
            entity.HasIndex(e => e.UserId);

            entity.Ignore(e => e.IsActive);
        });
    }

    private void ConfigureAuditLog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLogs");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.EntityType)
                .HasMaxLength(50);
            
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50);
            
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500);

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((User)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            ((User)entry.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }
 
}
