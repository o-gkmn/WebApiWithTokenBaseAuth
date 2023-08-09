using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using System.Security.Claims;
using WebApi.Constants;

namespace Services
{
    public class RoleManager : IRoleService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IAccesTokenManager _accesTokenManager;

        public RoleManager(IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager, ILoggerService logger, IAccesTokenManager accesTokenManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _accesTokenManager = accesTokenManager;
        }

        public async Task<bool> CreateRoleAsync(RoleDtoForInsertion roleDtoForInsertion)
        {
            var role = _mapper.Map<Role>(roleDtoForInsertion);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return true;
            else
            {
                _logger.LogError(result.Errors.FirstOrDefault().Description);
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return true;
            else
            {
                _logger.LogError(result.Errors.FirstOrDefault().Description);
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _roleManager.Roles.AsEnumerable();
            return roles;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }
            return role;
        }

        public async Task<bool> UpdateRoleAsync(string name, RoleDtoForUpdate roleDtoForUpdate)
        {
            var role = await _roleManager.FindByNameAsync(name);

            if (role == null)
            {
                _logger.LogError($"{name} could not found");
                return false;
            }

            _mapper.Map(roleDtoForUpdate, role);

            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetRolesForUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                _logger.LogError($"{userName} could not found");
                throw new UserNotFoundException(userName);
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<List<UserDto>> GetUsersInRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var userDtoList = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDtoList.Add(userDto);
            }
            return userDtoList;
        }

        public async Task<bool> AddRoleToUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user is null)
            {
                _logger.LogError($"{userName} could not found");
                throw new UserNotFoundException(userName);
            }

            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleFromUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user is null)
            {
                _logger.LogError($"{userName} could not found");
                throw new UserNotFoundException(userName);
            }

            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase))
            {
                _logger.LogError($"{roleName} could not found for {userName}");
                throw new RoleNotFoundForUser(roleName, userName);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> GivePermissionToRole(string roleName, string perm)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(role.Name.ToString());
            }

            var claims = await _roleManager.GetClaimsAsync(role);

            if (!CheckPermissionIsExists(perm))
            {
                _logger.LogError($"{perm} could not found for {roleName}");
                return false;
            }

            if (claims.Any(c => c.Type.Equals(perm)))
                return true;

            var result = await _roleManager.AddClaimAsync(role, new Claim(perm, "true"));
            return result.Succeeded;
        }

        public async Task<bool> RemovePermissionFromRole(string roleName, string perm)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }

            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
            {
                if (claim.Type.Equals(perm))
                {
                    var result = await _roleManager.RemoveClaimAsync(role, claim);
                    return result.Succeeded;
                }
            }
            return false;
        }

        public async Task<IEnumerable<string>> GetAllPermissionsInRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                _logger.LogError($"{roleName} could not found");
                throw new RoleNotFoundException(roleName);
            }

            var claims = await _roleManager.GetClaimsAsync(role);
            return claims.Select(c => c.Type);
        }

        public async Task<Role?> DecodeRoleFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Token is empty");
                return null;
            }

            var principal = _accesTokenManager.GetPrincipal(token);

            if (principal is null)
            {
                _logger.LogError($"Principal is not get from token : Token : {token}");
                return null;
            }

            foreach (var claim in principal.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    var role = await _roleManager.FindByNameAsync(claim.Value);

                    if (role is null)
                    {
                        _logger.LogWarning($"Role not found : null");
                        throw new RoleNotFoundException("null");
                    }

                    return role;
                }
            }

            return null;
        }

        private bool CheckPermissionIsExists(string perm)
        {
            foreach (var permission in Permissions.PermissionsList)
            {
                if (permission.Equals(perm))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
