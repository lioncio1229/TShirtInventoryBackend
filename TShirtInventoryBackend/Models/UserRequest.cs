﻿namespace TshirtInventoryBackend.Models
{
    public class UserInputCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserAddInputs
    {
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateInputs
    {
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoleUpdateInputs
    {
        public int RoleId { get; set; }
    }

    public class UserRegistrationCredentials
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
