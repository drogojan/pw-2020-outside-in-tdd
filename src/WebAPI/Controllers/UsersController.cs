using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Application.Users.Commands.RegisterUser;

namespace WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserCommand request)
        {
            var registeredUserVm = await Mediator.Send(request);

            return Created("", registeredUserVm);
        }
    }
}
