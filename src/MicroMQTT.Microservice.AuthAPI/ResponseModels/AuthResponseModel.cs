using System;
using System.Collections.Generic;

namespace MicroMQTT.Microservice.AuthAPI
{
    public class AuthResponseModel
    {
        public bool IsAuthenticated { get; set; }

        public IEnumerable<string> SubscribeAccess { get; set; }

        public IEnumerable<string> PublishAccess { get; set; }
    }
}
