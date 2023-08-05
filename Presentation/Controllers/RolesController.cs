using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using WebApi.Constants;

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

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanReadRoles })]
        [HttpGet("roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _service.RoleService.GetAllRoles();
            return Ok(roles);
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanReadRoles })]
        [HttpGet("{role}")]
        public async Task<IActionResult> GetRoleByNameAsync([FromRoute(Name = "role")] string roleName)
        {
            roleName = roleName.Replace("_", " ");
            var role = await _service.RoleService.GetRoleByNameAsync(roleName);
            return Ok(role);
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanCreateRoles })]
        [HttpPost("create_role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDtoForInsertion roleDtoForInsertion)
        {
            await _service.RoleService.CreateRoleAsync(roleDtoForInsertion);
            return Ok();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanDeleteRoles })]
        [HttpPost("delete_role")]
        public async Task<IActionResult> DeleteRoleAsync([FromBody()] RoleDtoForInsertion roleDtoForInsertion)
        {
            await _service.RoleService.DeleteRoleAsync(roleDtoForInsertion.Name);
            return Ok();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanUpdateRoles })]
        [HttpPut("update_role/{role}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute(Name = "role")] string role, [FromBody] RoleDtoForUpdate roleDtoForUpdate)
        {
            role = role.Replace("_", " ");
            var result = await _service.RoleService.UpdateRoleAsync(role, roleDtoForUpdate);
            return Ok();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanReadRolesInUser })]
        [HttpGet("get_roles/{user}")]
        public async Task<IActionResult> GetRolesForUserAsync([FromRoute(Name = "user")] string user)
        {
            var roles = await _service.RoleService.GetRolesForUserAsync(user);
            return Ok(roles);
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanReadRolesInUser })]
        [HttpGet("get_users/{role}")]
        public async Task<IActionResult> GetUsersInRoleAsync([FromRoute(Name = "role")] string role)
        {
            role = role.Replace("_", " ");
            var users = await _service.RoleService.GetUsersInRoleAsync(role);
            return Ok(users);
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanAssignRoleToUser })]
        [HttpPost("add_role_to_user")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.AddRoleToUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanDeleteRoleFromUser })]
        [HttpPost("delete_role_from_user")]
        public async Task<IActionResult> DeleteRoleFromUserAsync([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _service.RoleService.DeleteRoleFromUserAsync(userRoleDto.userName, userRoleDto.roleName);
            return result ? Ok() : BadRequest();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanGivePermissionToRole })]
        [HttpPost("give_permission_to_role")]
        public async Task<IActionResult> GivePermissionToRole([FromBody] PermissionDto permissionDto) 
        {
            var result = await _service.RoleService.GivePermissionToRole(permissionDto.Role, permissionDto.Permission);
            return result ? Ok() : BadRequest();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanRemovePermissionFromRole })]
        [HttpPost("remove_permission_from_role")]
        public async Task<IActionResult> RemovePermissionFromRole([FromBody] PermissionDto permissionDto)
        {
            var result = await _service.RoleService.RemovePermissionFromRole(permissionDto.Role, permissionDto.Permission);
            return result ? Ok() : BadRequest();
        }

        [TypeFilter(typeof(AutherizationFilter), Arguments = new object[] { Permissions.CanReadPermissions })]
        [HttpGet("get_all_permission_in_role/{role}")]
        public async Task<IActionResult> GetAllPermissionsInRole([FromRoute(Name = "role")] string role)
        {
            role = role.Replace("_", " ");
            var result = await _service.RoleService.GetAllPermissionsInRole(role);
            return Ok(result);
        }
    }
}
