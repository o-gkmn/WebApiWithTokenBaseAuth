﻿namespace Entities.DataTransferObject
{
    public record UserDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }
        public bool PhoneNumberConfirmed { get; init; }
        public bool TwoFactorEnabled { get; init; }

    }
}
