using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {        
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")] // Post: api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.UserName))
            return BadRequest("Username is taken");


        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key //Salt
        };

        //Save user in memory for ef
        _context.Users.Add(user);

        //Save user to the database for ef
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
        };

    }

    [HttpPost("login")] // Post: api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        
        if(user == null)
            return Unauthorized("Invalid Username");

        if (user.PasswordSalt == null)
        {
            return Unauthorized("Unauthorized");
        }            

        //Salt coming from the database for the user returned
        using var hmac = new HMACSHA512(user.PasswordSalt);

        //Get the hash for the password coming from the site using the salt from the user in the database
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //The computed hash generated for the pass coming from the site needs to be the same
        //as the password in the database
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid Password");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };

    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}
