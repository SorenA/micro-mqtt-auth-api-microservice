using MicroMQTT.Microservice.AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MicroMQTT.Microservice.AuthAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly AppDbContext dbContext;

        private bool isAuthTokenEnabled { get; set; }
        private string authToken { get; set; }

        public AuthController(ILogger<AuthController> logger, AppDbContext dbContext, IConfiguration config)
        {
            this.logger = logger;
            this.dbContext = dbContext;

            authToken = config.GetValue<string>("AuthToken");
            isAuthTokenEnabled = !string.IsNullOrEmpty(authToken);

        }

        [HttpPost]
        public AuthResponseModel Post(AuthRequestModel model)
        {
            // Check if we should match auth token
            if (isAuthTokenEnabled && authToken != model.AuthToken)
            {
                return new AuthResponseModel(false);
            }

            // Find user with matching username
            var user = dbContext.MqttUsers
                .Where(x => x.Username == model.Username)
                .FirstOrDefault();

            // Check if user is found
            if (user == null)
            {
                return new AuthResponseModel(false);
            }

            // Check for password match
            if (user.Password != model.Password)
            {
                return new AuthResponseModel(false);
            }

            // Fetch ACL
            var userAcl = dbContext.MqttUserAccessControlListItems
                .Where(x => x.MqttUserId == user.Id)
                .ToList();

            return new AuthResponseModel()
            {
                IsAuthenticated = true,
                PublishAccess = userAcl
                    .Where(x => x.Type == MqttUserAccessControlListItem.TypePublish)
                    .Select(x => x.TopicPattern)
                    .ToList(),
                SubscribeAccess = userAcl
                    .Where(x => x.Type == MqttUserAccessControlListItem.TypeSubscibe)
                    .Select(x => x.TopicPattern)
                    .ToList(),
            };
        }
    }
}
