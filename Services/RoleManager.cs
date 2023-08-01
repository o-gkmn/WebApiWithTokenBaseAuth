using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class RoleManager : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleManager(IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateRoleAsync(RoleDtoForInsertion roleDtoForInsertion)
        {
            var role = _mapper.Map<Role>(roleDtoForInsertion);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return true;
            else
                throw new Exception(result.Errors.FirstOrDefault().Description);
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new RoleNotFoundException(roleName);
            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return true;
            else
                throw new Exception(result.Errors.FirstOrDefault().Description);
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
                throw new RoleNotFoundException(roleName);
            return role;
        }

        public async Task<bool> UpdateRoleAsync(string name, RoleDtoForUpdate roleDtoForUpdate)
        {
            var role = await _roleManager.FindByNameAsync(name);

            if (role == null)
            {
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
                throw new UserNotFoundException(userName);

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<List<UserDto>> GetUsersInRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new RoleNotFoundException(roleName);
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
                throw new UserNotFoundException(userName);

            if (role is null)
                throw new RoleNotFoundException(roleName);

            var result = await _userManager.AddToRoleAsync(user, roleName);
            
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleFromUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user is null)
                throw new UserNotFoundException(userName);

            if (role is null)
                throw new RoleNotFoundException(roleName);

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(roleName, StringComparer.InvariantCultureIgnoreCase))
                throw new RoleNotFoundForUser(roleName, userName);
            
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

    }
}
