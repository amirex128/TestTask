using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Auth;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private AuthApplication _authApplication;

        public AuthController(AuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_authApplication.CheckAuth(model))
            {
                return Ok(new
                {
                    token = _authApplication.GetToken(model.Username),
                    message = "You are login"
                });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Login(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_authApplication.RegisterUser(model))
            {
                return Ok("Register is Successful");
            }

            return Ok(new
            {
                message = "This Username already exists"
            });
        }
    }
}