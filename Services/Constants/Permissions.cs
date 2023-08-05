using System.Collections.Generic;

namespace WebApi.Constants
{
    public static class Permissions
    {
        public const string CanReadRoles = "CanReadRoles";
        public const string CanCreateRoles = "CanCreateRoles";
        public const string CanDeleteRoles = "CanDeleteRoles";
        public const string CanUpdateRoles = "CanUpdateRoles";


        public const string CanReadRolesInUser = "CanReadRolesInUser";
        public const string CanAssignRoleToUser = "CanAssignRoleToUser";
        public const string CanDeleteRoleFromUser = "CanDeleteRoleFromUser";
        public const string CanReadPermissions = "CanReadPermissions";
        public const string CanGivePermissionToRole = "CanGivePermissionToRole";
        public const string CanRemovePermissionFromRole = "CanRemovePermissionFromRole";

        public static List<string> PermissionsList = new List<string>()
        {
            CanReadRoles,
            CanCreateRoles,
            CanDeleteRoles,
            CanUpdateRoles,
            CanReadRolesInUser,
            CanAssignRoleToUser,
            CanDeleteRoleFromUser,
            CanReadPermissions,
            CanGivePermissionToRole,
            CanRemovePermissionFromRole
        };
    }
}
