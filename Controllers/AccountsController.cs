using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _sigInManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  IConfiguration configuration)
        {
           _userManager=userManager;
           _sigInManager=signInManager;
           _configuration=configuration;
        }

        [HttpPost("")]
        public async Task<ActionResult<UserToken>> AddUser([FromBody] UserInfo userInfo)
        {
            var user = new ApplicationUser
            {
                UserName = userInfo.Email,
                Email = userInfo.Email
            };

            var result= await _userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                return BuildToken(userInfo, new List<string>());
            }
            else
            {
                return BadRequest("User name or password invalid");
            }
        }

        private UserToken BuildToken(UserInfo userInfo, List<string> list)
        {
            return new UserToken();
        }
    }
}