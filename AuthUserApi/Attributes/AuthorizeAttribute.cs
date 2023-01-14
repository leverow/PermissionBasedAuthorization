using AuthUserApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthUserApi.Attributes;

public class AuthorizeAttribute : TypeFilterAttribute
{
	public AuthorizeAttribute(Permission permission = Permission.None) : base(typeof(AuthorizePermissionAttribute))
	{
		Arguments = new object[] { permission };
	}
}