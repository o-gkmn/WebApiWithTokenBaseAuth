# 🌐 Web Api With Token Base Authentication

This Project creates an acces token and refresh token and gives user so user can authenticate with this acces token. 
When acces token expired client send refresh token to server. 
Server validates this refresh token if this token is valid then server send a new acces token to user.

User has role or roles and roles has permissions. Using this permissions access to endpoint.
If user has not enoght level of authority for access to enpoint then cannot use this endpoint.

### 📦Packages
  - Entity Framework Core
  - Identity
  - Jwt Bearer
  - Auto Mapper
  - NLog

### Server
  - Microsoft SQL Server

### 🏁Endpoints
  1. **Authentication**
     - **POST** .../api/auth/register:
     - **POST** .../api/auth/login
     - **POST** .../api/auth/refresh
       
  3. **Roles**
     - **GET** .../api/roles/roles
     - **GET** .../api/roles/{role name}
     - **POST** .../api/roles/create_role
     - **POST** .../api/roles/delete_role
     - **PUT** .../api/roles/update_role/{role name}
     - **GET** .../api/roles/get_roles/{user name}
     - **GET** .../api/roles/get_users/{role name}
     - **POST** .../api/roles/add_role_to_user
     - **POST** .../api/roles/delete_role_from_user
     - **POST** .../api/roles/give_permission_to_role
     - **POST** .../api/roles/remove_permission_from_role
     - **GET** .../api/roles/get_all_permission_in_role/{role name}
     - **GET** .../api/roles/get_user_role_from_token
