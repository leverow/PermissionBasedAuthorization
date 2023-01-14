﻿using AuthUserApi.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthUserApi.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class AuthorizePermissionAttribute : ActionFilterAttribute, IAuthorizationFilter
{
    private readonly Permission _permission;
    private readonly AppDbContext _context;
    public AuthorizePermissionAttribute(AppDbContext context, Permission permission)
    {
        _context = context;
        _permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new ChallengeResult();
            return;
        }

        var claims = context.HttpContext.User;
        var userRole = claims.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var permissions = _context.Roles.FirstOrDefault(r => r.NormalizedName == userRole.ToUpper())?.Permissions;
        if (permissions is null || permissions?.Any(p => p == _permission) == false)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}