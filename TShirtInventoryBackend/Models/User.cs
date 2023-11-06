﻿using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Models
{
    public class User : IEntityPKEmail
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Role? Role { get; set; }
        public bool IsActived { get; set; }
    }
}
