using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RolesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _service.RoleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetRoleByNameAsync([FromRoute(Name = "roleName")] string roleName)
        {
            var role = await _service.RoleService.GetRoleByNameAsync(roleName);
            return Ok(role);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDtoForInsertion roleDtoForInsertion)
        {
            await _service.RoleService.CreateRoleAsync(roleDtoForInsertion);
            return Ok();
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRoleAsync([FromBody()] string roleName)
        {
            await _service.RoleService.DeleteRoleAsync(roleName);
            return Ok();
        }

        [HttpPut("UpdateRole/{roleName}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute(Name = "roleName")] string roleName, [FromBody] RoleDtoForUpdate roleDtoForUpdate)
        {
            var result = await _service.RoleService.UpdateRoleAsync(roleName, roleDtoForUpdate);
            return Ok();
        }

        [HttpGet("GetRolesForUser/{user}")]
        public async Task<IActionResult> GetRolesForUserAsync([FromRoute(Name = "user")] string user)
        {
            var roles = await _service.RoleService.GetRolesForUserAsync(user);
            return Ok(roles);
        }

        [HttpGet("GetUsersInRole/{role}")]
        public async Task<IActionResult> GetUsersInRoleAsync([FromRoute(Name = "role")] string role)
        {
            var users = await _service.RoleService.GetUsersInRoleAsync(role);
            return Ok(users);
        }

        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.AddRoleToUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }

        [HttpPost("DeleteRoleFromUser")]
        public async Task<IActionResult> DeleteRoleFromUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.DeleteRoleFromUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }
    }
}
