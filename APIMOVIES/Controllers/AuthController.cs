using Application.Users.ConfirmEmail;
using Application.Users.Register;
using Application.Users.SendEmailConfirmation;
using Domain.Users;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIMOVIES.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register", Name = "RegisterAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterAsync([FromBody] RegisterCommand command)
        {
            try
            {
                var created = await _sender.Send(command);
                return Ok(created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("confirm-email", Name = "ConfirmEmailAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConfirmEmailAsync([FromQuery] int userId, [FromQuery] string token)
        {
            try
            {
                var confirmed = await _sender.Send(new ConfirmEmailCommand { UserId = userId, Token = token });
                if (!confirmed)
                {
                    return BadRequest("The confirmation link is invalid or has expired.");
                }

                return Ok("Email confirmed successfully.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost("resend-email-confirmation", Name = "SendEmailConfirmationAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SendEmailConfirmationAsync([FromBody] SendEmailConfirmationCommand command)
        {
            try
            {
                await _sender.Send(command);
                return Ok("If the email is registered and not yet confirmed, a confirmation link has been sent.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
