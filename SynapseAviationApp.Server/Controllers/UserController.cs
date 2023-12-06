using Microsoft.AspNetCore.Mvc;
using SynapseAviationApp.Server.DTOModels;
using SynapseAviationApp.Server.Models;
using SynapseAviationApp.Server.Services.Interfaces;

namespace SynapseAviationApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserEditDto updatedUserData)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);                
            }
            var updatedUser = await _userService.UpdateUser(id, updatedUserData);
            return Ok(updatedUser);
        }
    }
}
