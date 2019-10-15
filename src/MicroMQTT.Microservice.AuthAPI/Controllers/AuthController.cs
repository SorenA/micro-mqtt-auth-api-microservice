using MicroMQTT.Microservice.AuthAPI.Models;
using MicroMQTT.Microservice.AuthAPI.RequestModels;
using MicroMQTT.Microservice.AuthAPI.ResponseModels;
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

        private bool IsAuthTokenEnabled { get; set; }
        private string AuthToken { get; set; }

        public AuthController(ILogger<AuthController> logger, AppDbContext dbContext, IConfiguration config)
        {
            this.logger = logger;
            this.dbContext = dbContext;

            AuthToken = config.GetValue<string>("AuthToken");
            IsAuthTokenEnabled = !string.IsNullOrEmpty(AuthToken);
        }

        [HttpPost]
        public AuthResponseModel Post(AuthRequestModel model)
        {
            // Check if we should match auth token
            if (IsAuthTokenEnabled && AuthToken != model.AuthToken)
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
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
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
