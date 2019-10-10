using MicroMQTT.Microservice.AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroMQTT.Microservice.AuthAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MqttUser> MqttUsers { get; set; }
        public DbSet<MqttUserAccessControlListItem> MqttUserAccessControlListItems { get; set; }
    }
}
