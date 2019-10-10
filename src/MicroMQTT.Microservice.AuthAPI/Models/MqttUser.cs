using System.ComponentModel.DataAnnotations.Schema;

namespace MicroMQTT.Microservice.AuthAPI.Models
{
    [Table("mqtt_users")]
    public class MqttUser
    {
        public ulong Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
