using Microsoft.EntityFrameworkCore;
using PivoChat.Models;

namespace PivoChat.Database
{
    public class ChatContext : DbContext
    {
        public DbSet<Chatroom> Chatroom { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> ChatMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Pivochat;Username=postgres;Password=0000");
        }

    }
}
