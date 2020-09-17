using System;

namespace WebAPI.AcceptanceTests.Models
{
    public class RegisteredUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
    }
}