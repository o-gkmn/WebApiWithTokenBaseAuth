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

        [HttpGet("roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _service.RoleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{role}")]
        public async Task<IActionResult> GetRoleByNameAsync([FromRoute(Name = "role")] string roleName)
        {
            roleName = roleName.Replace("_", " ");
            var role = await _service.RoleService.GetRoleByNameAsync(roleName);
            return Ok(role);
        }

        [HttpPost("create_role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDtoForInsertion roleDtoForInsertion)
        {
            await _service.RoleService.CreateRoleAsync(roleDtoForInsertion);
            return Ok();
        }

        [HttpPost("delete_role")]
        public async Task<IActionResult> DeleteRoleAsync([FromBody()] RoleDtoForInsertion roleDtoForInsertion)
        {
            await _service.RoleService.DeleteRoleAsync(roleDtoForInsertion.Name);
            return Ok();
        }

        [HttpPut("update_role/{role}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute(Name = "role")] string role, [FromBody] RoleDtoForUpdate roleDtoForUpdate)
        {
            role = role.Replace("_", " ");
            var result = await _service.RoleService.UpdateRoleAsync(role, roleDtoForUpdate);
            return Ok();
        }

        [HttpGet("get_roles/{user}")]
        public async Task<IActionResult> GetRolesForUserAsync([FromRoute(Name = "user")] string user)
        {
            var roles = await _service.RoleService.GetRolesForUserAsync(user);
            return Ok(roles);
        }

        [HttpGet("get_users/{role}")]
        public async Task<IActionResult> GetUsersInRoleAsync([FromRoute(Name = "role")] string role)
        {
            role = role.Replace("_", " ");
            var users = await _service.RoleService.GetUsersInRoleAsync(role);
            return Ok(users);
        }

        [HttpPost("add_role_to_user")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.AddRoleToUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }

        [HttpPost("delete_role_from_user")]
        public async Task<IActionResult> DeleteRoleFromUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.DeleteRoleFromUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }
    }
}
