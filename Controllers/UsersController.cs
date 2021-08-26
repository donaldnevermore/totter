using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly ApplicationContext context;
        private readonly IConfiguration config;
        private readonly ILogger<UsersController> logger;


        public UsersController(ApplicationContext context, IConfiguration config, ILogger<UsersController> logger) {
            this.context = context;
            this.config = config;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(long id) {
            var user = await context.Users.FindAsync(id);
            if (user is null) {
                return NotFound();
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User signupInfo) {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == signupInfo.UserName);
            if (user is not null) {
                return BadRequest();
            }

            user = new User {
                UserName = signupInfo.UserName,
                Password = signupInfo.Password
            };

            var token = GenerateJwt(user);

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { id = user.Id, token });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User loginInfo) {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == loginInfo.UserName);
            if (user is null) {
                return NotFound();
            }

            if (user.Password != loginInfo.Password) {
                return Unauthorized();
            }

            var token = GenerateJwt(user);
            // logger.Log(LogLevel.Information, "User {user.UserName} logged in.", user);
            return Ok(new { id = user.Id, token });
        }

        private string GenerateJwt(User userInfo) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claim is used to add identity to JWT token.
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("Date", DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(config["JwtAuth:Issuer"],
                config["JwtAuth:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
