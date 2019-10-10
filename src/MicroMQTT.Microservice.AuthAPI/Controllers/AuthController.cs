using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroMQTT.Microservice.AuthAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;

        public AuthController(ILogger<AuthController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public AuthResponseModel Post(AuthRequestModel model)
        {
            return new AuthResponseModel
            {
                IsAuthenticated = true,
                PublishAccess = new string[]
                {
                    "#",
                },
                SubscribeAccess = new string[]
                {
                    "#",
                },
            };
        }
    }
}
