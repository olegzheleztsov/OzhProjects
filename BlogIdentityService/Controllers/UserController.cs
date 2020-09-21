// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogIdentityService     File: UserController.cs    Created at 2020/09/20/10:28 PM
// All rights reserved, for personal using only
// 

using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogIdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserDto userInfo)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, userInfo.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return Problem(JsonSerializer.Serialize(result));
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(new ObjectResult("Hello"));
        }
    }
}