using System;
using Code2Gether_Discord_Bot.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Code2Gether_Discord_Bot.WebApi.DbContexts
{
    public class DiscordBotDbContext : DbContext
    {
        public DbSet<FakeModel> FakeModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=Code2GetherDiscordBot.db");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
