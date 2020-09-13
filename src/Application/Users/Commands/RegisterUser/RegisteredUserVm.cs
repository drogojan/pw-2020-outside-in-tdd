using System;

namespace OpenChat.Application.Users.Commands.RegisterUser
{
    public class RegisteredUserVm
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
    }
}