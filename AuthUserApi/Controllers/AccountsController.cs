using AuthUserApi.Data;
using AuthUserApi.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthUserApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    public AccountsController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserDto registerUserDto)
    {
        var user = new AppUser() { UserName = registerUserDto.UserName};

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        //await _userManager.AddToRoleAsync(user, "Superadmin");

        if (!result.Succeeded)
            return BadRequest();

        await _signInManager.SignInAsync(user, true);
        
        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}