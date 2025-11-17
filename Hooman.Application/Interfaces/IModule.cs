// namespace Hooman.Application.Interfaces;

// public class IModule
// {
//   // Module Management
//     Task<List<ModuleDto>> GetAllModulesAsync();
//     Task<ModuleDto> GetModuleByIdAsync(Guid moduleId);
//     Task<ModuleDto> CreateModuleAsync(string name, string displayName, string description, string? icon, string? routePrefix, int displayOrder);
//     Task<ModuleDto> UpdateModuleAsync(Guid moduleId, string displayName, string description, string? icon, string? routePrefix, int displayOrder);
//     Task<bool> DeleteModuleAsync(Guid moduleId);

//     // User Module Access
//     Task<UserModuleAccessDto> GetUserModuleAccessAsync(Guid userId);
//     Task<List<ModuleDto>> GetUserAccessibleModulesAsync(Guid userId);
//     Task<bool> UserCanAccessModuleAsync(Guid userId, string moduleName, string action = "view");
    
//     // Role Module Assignment
//     Task<bool> AssignModuleToRoleAsync(AssignModuleAccessRequest request, Guid grantedBy);
//     Task<bool> RemoveModuleFromRoleAsync(Guid roleId, Guid moduleId);
//     Task<List<ModuleDto>> GetRoleModulesAsync(Guid roleId);

//     // Direct User Module Access (Override)
//     Task<bool> GrantModuleAccessToUserAsync(GrantUserModuleAccessRequest request, Guid grantedBy);
//     Task<bool> RevokeModuleAccessFromUserAsync(Guid userId, Guid moduleId);
//     Task<List<ModuleDto>> GetUserDirectModuleAccessAsync(Guid userId);
// }
