using System;
using System.Reflection;
using Code2Gether_Discord_Bot.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Code2Gether_Discord_Bot.WebApi.DbContexts
{
    public class DiscordBotDbContext : DbContext
    {
        #region DbSets
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<UserRole> ProjectUserProjectRoles { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=Code2GetherDiscordBot.db", o =>
            {
                o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Methods
        public DbSet<Code2Gether_Discord_Bot.Library.Models.UserRole> UserRoles { get; set; }
        #endregion
    }
}
