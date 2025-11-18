using Hooman.Application.DTO.Auth;
using Hooman.Application.Interfaces;
using Hooman.Domain.Entities.Identity;
using Hooman.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hooman.Application.Services;

public class ModuleService : IModuleService
{
  private readonly ApplicationDbContext _context;

  public ModuleService(ApplicationDbContext context)
  {
    _context = context;
  }

  // Module Management
  public async Task<ModuleDto> CreateModuleAsync(CreateModuleRequest request)
  {
    var existingModule = await _context.Modules
      .FirstOrDefaultAsync(m => m.Code == request.Code || m.Name == request.Name);
  
    if(existingModule != null) 
      throw new Exception("Module with this code or name already exists");

    // Map Available Permissions
    var modulePermissions = request.AvailablePermissions.Select(mp => new ModulePermission
    {
      Name = mp,
      DisplayName = mp
    }).ToList();

    var module = new Module
    {
      Id = Guid.NewGuid(),
      Name = request.Name,
      Code = request.Code,
      Description = request.Description,
       Icon = request.Icon,
            Route = request.Route,
            CategoryId = request.CategoryId,
            ParentModuleId = request.ParentModuleId,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            RequiresAuth = request.RequiresAuth,
            AvailablePermissions = modulePermissions,
            CreatedAt = DateTime.UtcNow
    };

    await _context.Modules.AddAsync(module);
    await _context.SaveChangesAsync();

    return await GetModuleByIdAsync(module.Id);
  }

  public async Task<ModuleDto> UpdateoduleAsync(Guid moduleId, UpdateModuleRequest request)
  {
    var module = await _context.Modules.FindAsync(moduleId);

    if(module == null) 
      throw new Exception("Module not found");

    var existingModule = await _context.Modules
      .FirstOrDefaultAsync(m => m.Id != moduleId &&
                        (m.Code == request.Code || m.Name == request.Name));
  
    if(existingModule != null)
      throw new Exception("MModule with this code or name already exists.");

    // Map Available Permissions
   module.Name = request.Name;
    module.Code = request.Code;
    module.Description = request.Description;
    module.Icon = request.Icon;
    module.Route = request.Route;
    module.CategoryId = request.CategoryId;
    module.ParentModuleId = request.ParentModuleId;
    module.DisplayOrder = request.DisplayOrder;
    module.RequiresAuth = request.RequiresAuth;
    module.UpdatedAt = DateTime.UtcNow;

    module.AvailablePermissions.Clear();

    // Add new ModulePermission entities based on the request strings
    foreach (var pName in request.AvailablePermissions)
    {
        module.AvailablePermissions.Add(new ModulePermission
        {
            // You might need to check if the permission already exists in a static list
            Name = pName,
            DisplayName = pName, // Use the name as display name for simplicity
            // ModuleId is usually handled by EF Core when added to the collection
        });
    }

    await _context.SaveChangesAsync();

    return await GetModuleByIdAsync(module.Id);
  }

  public async Task<ModuleDto> GetModuleByIdAsync(Guid moduleId)
  {
    var module = await _context.Modules
      .Include(m => m.Category)
      .Include(m => m.ParentModule)
      .Include(m => m.ChildModules)
      .FirstOrDefaultAsync(m => m.Id == moduleId);

    if( module == null)
      throw new Exception("Module not found");
    
    return MapToModuleDto(module);
  }

  private ModuleDto MapToModuleDto(Module module)
    {
        return new ModuleDto(
            module.Id,
            module.Name,
            module.Code!,
            module.Description,
            module.Icon,
            module.Route,
            module.Category != null ? new ModuleCategoryDto(
                module.Category.Id,
                module.Category.Name,
                module.Category.Description,
                module.Category.DisplayOrder,
                0
            ) : null,
            module.ParentModuleId,
            module.ParentModule?.Name,
            module.DisplayOrder,
            module.IsActive,
            module.RequiresAuth,
            module.AvailablePermissions,
            null, // GrantedPermissions - set by caller
            null, // AccessSource - set by caller
            null, // ExpiresAt - set by caller
            module.ChildModules.Select(cm => new ModuleDto(
                cm.Id,
                cm.Name,
                cm.Code,
                cm.Description,
                cm.Icon,
                cm.Route,
                null,
                cm.ParentModuleId,
                null,
                cm.DisplayOrder,
                cm.IsActive,
                cm.RequiresAuth,
                cm.AvailablePermissions,
                null,
                null,
                null,
                new List<ModuleDto>()
            )).ToList()
        );
    }

}
