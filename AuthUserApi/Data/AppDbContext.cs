using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthUserApi.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, ulong>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var superAdminPermissions = new List<Permission>()
        {
            Permission.CanCreateUser,
            Permission.CanReadUser,
            Permission.CanUpdateUser,
            Permission.CanDeleteUser,
        };
        builder.Entity<AppRole>().HasData(new AppRole()
        {
            Id= 1,
            Name = "Superadmin",
            NormalizedName= "Superadmin".ToUpper(),
            ConcurrencyStamp = "c1a8014c-1cb4-492f-92b5-d55b740d4844",
            Permissions = superAdminPermissions
        });
        var appUser = new AppUser()
        {
            Id = 1,
            UserName = "leverow",
            NormalizedUserName = "LEVEROW"
        };
        //set user password
        PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "leverow123");

        builder.Entity<AppUser>().HasData(appUser);

        builder.Entity<IdentityUserRole<ulong>>().HasData(new IdentityUserRole<ulong>
        {
            RoleId = 1,
            UserId = 1
        });
    }
}