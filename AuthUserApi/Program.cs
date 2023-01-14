using AuthUserApi.Data;
using AuthUserApi.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;database=authsample"));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();

/*builder.Services.AddSingleton<UserManager<AppUser>>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();*/
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
