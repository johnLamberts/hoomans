using Hooman.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hooman.Infrastructure.Data;
public static class IdentitySeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Check if already seeded
        if (await context.Roles.AnyAsync())
            return;

        // Seed Roles
        var roles = new List<Role>
        {
            new() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", 
                Description = "System Administrator with full access", IsSystemRole = true },
            new() { Id = Guid.NewGuid(), Name = "Teacher", NormalizedName = "TEACHER", 
                Description = "Course instructor and content creator", IsSystemRole = true },
            new() { Id = Guid.NewGuid(), Name = "Student", NormalizedName = "STUDENT", 
                Description = "Learner with access to enrolled courses", IsSystemRole = true },
            new() { Id = Guid.NewGuid(), Name = "Moderator", NormalizedName = "MODERATOR", 
                Description = "Content moderator and community manager", IsSystemRole = true },
            new() { Id = Guid.NewGuid(), Name = "Staff", NormalizedName = "STAFF", 
                Description = "Staff member with limited administrative access", IsSystemRole = true }
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();

        // Seed Permissions
        var permissions = new List<Permission>
        {
            // User Management
            new() { Id = Guid.NewGuid(), Name = "users.view", NormalizedName = "USERS.VIEW", 
                Category = "User", Description = "View user profiles", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "users.create", NormalizedName = "USERS.CREATE", 
                Category = "User", Description = "Create new users", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "users.edit", NormalizedName = "USERS.EDIT", 
                Category = "User", Description = "Edit user information", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "users.delete", NormalizedName = "USERS.DELETE", 
                Category = "User", Description = "Delete users", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "users.manage_roles", NormalizedName = "USERS.MANAGE_ROLES", 
                Category = "User", Description = "Assign and remove user roles", IsSystemPermission = true },
            
            // Course Management
            new() { Id = Guid.NewGuid(), Name = "courses.view", NormalizedName = "COURSES.VIEW", 
                Category = "Course", Description = "View courses", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "courses.create", NormalizedName = "COURSES.CREATE", 
                Category = "Course", Description = "Create new courses", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "courses.edit", NormalizedName = "COURSES.EDIT", 
                Category = "Course", Description = "Edit course content", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "courses.delete", NormalizedName = "COURSES.DELETE", 
                Category = "Course", Description = "Delete courses", IsSystemPermission = true },
            new() { Id = Guid.NewGuid(), Name = "courses.publish", NormalizedName = "COURSES.PUBLISH", 
                Category = "Course", Description = "Publish/unpublish courses", IsSystemPermission = true },
            
            // Add more permissions...
        };

        await context.Permissions.AddRangeAsync(permissions);
        await context.SaveChangesAsync();

        // Assign Permissions to Roles
        var adminRole = roles.First(r => r.NormalizedName == "ADMIN");
        var adminPermissions = permissions.Select(p => new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = adminRole.Id,
            PermissionId = p.Id
        });

        await context.RolePermissions.AddRangeAsync(adminPermissions);
        await context.SaveChangesAsync();
    }
}
