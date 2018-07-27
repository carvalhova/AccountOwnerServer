using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccountOwnerServer.Infra;
using AccountOwnerServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AccountOwnerServer.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.UserName == "user" && user.Password == "pwd@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurity.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = GetClaims(user.UserName);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        private IList<Claim> GetClaims(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "Manager"),
                new Claim(ClaimTypes.Role, "Operator")
            };

            return claims;
        }
    }
}