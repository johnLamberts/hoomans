
using Hooman.Application.DTO.Auth;

namespace Hooman.Application.Interfaces;

public interface IModuleService
{
  // Module Management
    Task<List<ModuleDto>> GetAllModulesAsync();
    Task<ModuleDto> GetModuleByIdAsync(Guid moduleId);
    Task<ModuleDto> CreateModuleAsync(string name, string displayName, string description, string? icon, string? routePrefix, int displayOrder);
    Task<ModuleDto> UpdateModuleAsync(Guid moduleId, string displayName, string description, string? icon, string? routePrefix, int displayOrder);
    Task<bool> DeleteModuleAsync(Guid moduleId);

    // User Module Access
    Task<UserModuleAccessDto> GetUserModuleAccessAsync(Guid userId);
    Task<List<ModuleDto>> GetUserAccessibleModulesAsync(Guid userId);
    Task<bool> UserCanAccessModuleAsync(Guid userId, string moduleName, string action = "view");
    
    // Role Module Assignment
Task<bool> AssignModuleToRoleAsync(Guid roleId, Guid moduleId, List<string> permissions);
Task<bool> RemoveModuleFromRoleAsync(Guid roleId, Guid moduleId);
Task<bool> UpdateRoleModulePermissionsAsync(Guid roleId, Guid moduleId, List<string> permissions);
Task<List<ModuleDto>> GetRoleModulesAsync(Guid roleId);
Task<List<RoleModuleAccessDto>> GetModuleRolesAsync(Guid moduleId);

// Direct User Module Access (Override)
Task<bool> GrantUserModuleAccessAsync(Guid userId, Guid moduleId, List<string> permissions, DateTime? expiresAt = null, string? grantedBy = null);
Task<bool> RevokeUserModuleAccessAsync(Guid userId, Guid moduleId);
Task<bool> UpdateUserModulePermissionsAsync(Guid userId, Guid moduleId, List<string> permissions);
Task<List<ModuleDto>> GetUserModulesAsync(Guid userId, bool includeRoleModules = true);
Task<List<UserModuleAccessDto>> GetModuleUsersAsync(Guid moduleId);

// Permission Checks
Task<bool> UserHasModuleAccessAsync(Guid userId, string ModuleCode);
Task<bool> UserHasModulePermissionAsync(Guid userId, string moduleCode, string permission);
Task<List<string>> GetUserModulePermissionAsync(Guid userId, string moduleCode);
Task<Dictionary<string, List<string>>> GetAllUserModulePermissionsAsync(Guid userId);

// Module Categories
Task<ModuleCategoryDto> CreateCategoryAsync(string name, string? description, int displayOrder);
    Task<ModuleCategoryDto> UpdateCategoryAsync(Guid categoryId, string name, string? description, int displayOrder);
    Task<bool> DeleteCategoryAsync(Guid categoryId);
    Task<List<ModuleCategoryDto>> GetAllCategoriesAsync();


    
}

