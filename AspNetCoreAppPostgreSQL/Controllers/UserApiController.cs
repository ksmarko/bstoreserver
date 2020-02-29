using AspNetCoreAppPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AspNetCoreAppPostgreSQL.Controllers
{
    [ApiController]
    [Route("/api")]
    public class UserApiController : ControllerBase
    {
        private readonly TvShowsContext _context;

        public UserApiController(TvShowsContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginRequest request)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existing == null)
                return BadRequest("Invalid email or password");

            return existing;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<User>> Register(RegisterRequest request)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existing != null)
                return BadRequest("User with the same email already exists");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }

    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
