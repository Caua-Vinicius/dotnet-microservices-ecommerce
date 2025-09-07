using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.Auth
{
    public class Register
    {
        public class RegisterCommand  : IRequest<int>
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<RegisterCommand , int>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(RegisterCommand  request, CancellationToken cancellationToken)
            {
                var userExists = await _context.Users.AnyAsync(user => user.Email == request.Email);
                if (userExists) throw new Exception("User with this email already exists.");

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var newUser = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);
                return newUser.Id;
            }
        }
    }
}