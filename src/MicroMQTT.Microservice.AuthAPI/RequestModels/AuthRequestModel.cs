namespace MicroMQTT.Microservice.AuthAPI
{
    public class AuthRequestModel
    {
        public string AuthToken { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
