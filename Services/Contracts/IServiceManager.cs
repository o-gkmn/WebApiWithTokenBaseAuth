﻿namespace Services.Contracts
{
    public interface IServiceManager
    {
        public IAuthService AuthenticationService { get; }
        public IRoleService RoleService { get; }
    }
}
