using Entities.DataTransferObject;
using Entities.Models;

namespace Services.Contracts
{
    public interface IRoleService
    {

        IEnumerable<Role> GetAllRoles();
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<bool> CreateRoleAsync(RoleDtoForInsertion roleDtoForInsertion);
        Task<bool> UpdateRoleAsync(string roleName, RoleDtoForUpdate roleDtoForUpdate);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> AddRoleToUserAsync(string userName, string roleName);
        Task<bool> DeleteRoleFromUserAsync(string userName, string roleName);
        Task<IEnumerable<string>> GetRolesForUserAsync(string userName);
        Task<List<UserDto>> GetUsersInRoleAsync(string roleName);
    }
}
