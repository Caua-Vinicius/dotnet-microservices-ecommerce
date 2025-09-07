using MediatR;
using Microsoft.AspNetCore.Mvc;
using static AuthService.Features.Auth.Login;
using static AuthService.Features.Auth.Register;

namespace AuthService.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Register), new { id = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token });
            }
            catch (Exception exception)
            {
                return Unauthorized(new { message = exception.Message });
            }
        }
    }
}