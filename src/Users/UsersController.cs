namespace Totter.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Totter.Models;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly AppDbContext db;
    private readonly IConfiguration config;
    private readonly IMapper mapper;

    public UsersController(AppDbContext db, IConfiguration config, IMapper mapper) {
        this.db = db;
        this.config = config;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDTO>> GetUser(long id) {
        var user = await db.Users.FindAsync(id);
        if (user is null) {
            return NotFound();
        }

        var dto = mapper.Map<GetUserDTO>(user);
        return dto;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddUser(LoginDTO signupInfo) {
        var user = await db.Users.SingleOrDefaultAsync(u => u.Username == signupInfo.Username);
        if (user is not null) {
            // TODO: return "already has one"
            return BadRequest();
        }

        user = new User {
            Username = signupInfo.Username,
            Password = signupInfo.Password,
            Name = signupInfo.Username,
            LastLoggedIn = DateTime.Now
        };

        var token = GenerateJwt(user);

        db.Users.Add(user);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { id = user.Id, token });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginInfo) {
        var user = await db.Users.SingleOrDefaultAsync(u => u.Username == loginInfo.Username);
        if (user is null) {
            return NotFound();
        }

        if (user.Password != loginInfo.Password) {
            return Unauthorized();
        }

        var token = GenerateJwt(user);
        user.LastLoggedIn = DateTime.Now;

        try {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!UserExists(user.Id)) {
            return NotFound();
        }

        return Ok(new { id = user.Id, token });
    }

    // [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateUser(long id, UpdateUserDTO userInfo) {
        var user = await db.Users.FindAsync(id);
        if (user is null) {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(userInfo.Name)) {
            user.Name = userInfo.Name;
        }

        if (!string.IsNullOrEmpty(userInfo.Email)) {
            user.Email = userInfo.Email;
        }

        if (!string.IsNullOrEmpty(userInfo.Avatar)) {
            user.Avatar = userInfo.Avatar;
        }

        try {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!UserExists(id)) {
            return NotFound();
        }

        return NoContent();
    }

    private string GenerateJwt(User userInfo) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtAuth:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Claim is used to add identity to JWT token.
        var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("Date", DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken(config["JwtAuth:Issuer"],
            config["JwtAuth:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(240),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool UserExists(long id) {
        return db.Users.Any(e => e.Id == id);
    }
}
