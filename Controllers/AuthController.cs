using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webproj.Services;
using Webproj.Repositories;
using Webproj.Models;
using Webproj.Models.Requests;
using Webproj.Models.Responses;

namespace Webproj.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IUserRepository userRepository, 
                              IPasswordHasher passwordHasher,
                              IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // POST: /api/v1/auth/signUp
        [HttpPost("signUp")]
        public IActionResult SignUp(SignUpRequest request)
        {
            if (_userRepository.GetByEmail(request.email) == null)
            {
                string password = _passwordHasher.Hash(request.password);

                var user = new User 
                {
                    Email = request.email,
                    Nickname = request.nickname,
                    Password = password,
                    CreatedAt = DateTime.Now
                };

                _userRepository.Create(user);

                return Ok();
            }

            return BadRequest("User with given email is already exist.");
        }

        // POST: /api/v1/auth/signIn
        [HttpPost("signIn")]
        public IActionResult SignIn(SignInRequest request)
        {
            var user = _userRepository.GetByEmail(request.email);

            if (user != null)
            {
                if (!_passwordHasher.Check(user.Password, request.password))
                {
                    return BadRequest("The password or email is incorrect");
                }

                var token = _jwtTokenGenerator.Generate(user);

                var response = new SignInResponse(user, token);

                return Ok(response);
            }

            return BadRequest("User with this email does not exist.");
        }
    }
}