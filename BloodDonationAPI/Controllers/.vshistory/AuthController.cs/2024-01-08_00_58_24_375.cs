﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        /*private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: auth/login
        [HttpPost]
        public IActionResult Login([FromBody] LoginUser loginUser)
        {
            if (String.IsNullOrEmpty(loginUser.Email))
            {
                return BadRequest(new { message = "Email address needs to entered" });
            }
            else if (String.IsNullOrEmpty(loginUser.Password))
            {
                return BadRequest(new { message = "Password needs to entered" });
            }

            UserModel? loggedInUser = _authService.LoginUser(loginUser.Email, loginUser.Password);


            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { message = "User login unsuccessful" });
        }*/
    }
}
