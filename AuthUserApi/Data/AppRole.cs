using Microsoft.AspNetCore.Identity;

namespace AuthUserApi.Data;

public class AppRole : IdentityRole<ulong>
{
    public List<Permission>? Permissions { get; set; }
}