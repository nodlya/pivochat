using Microsoft.EntityFrameworkCore;
using PivoChat.Models;

namespace PivoChat.Database
{
    public class ChatContext : DbContext
    {
        public DbSet<Chatroom> Chatroom { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> ChatMessages { get; set; }

        public ChatContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=pivochat;Port=5432;Database=Pivochat;Username=postgres;Password=postgres");
        }

    }
}
