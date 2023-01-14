using AuthUserApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace AuthUserApi.Handlers;
public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission Permission { get; set; }

    public PermissionRequirement(Permission permission = Permission.None)
    {
        Permission = permission;
    }
}