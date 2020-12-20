using System;
using System.Reflection;
using Code2Gether_Discord_Bot.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Code2Gether_Discord_Bot.WebApi.DbContexts
{
    public class DiscordBotDbContext : DbContext
    {
        #region Fields
        public static readonly ILoggerFactory factory = LoggerFactory.Create(x => x.AddConsole());
        #endregion

        #region DbSets
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite(@"DataSource=Code2GetherDiscordBot.db", o =>
                    {
                        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    })
                    .UseLoggerFactory(factory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectMember>()
                .HasKey(x => new { x.MemberID, x.ProjectID });

            modelBuilder.Entity<ProjectMember>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(x => x.ProjectID);

            modelBuilder.Entity<ProjectMember>()
                .HasOne<Member>()
                .WithMany()
                .HasForeignKey(x => x.MemberID);
        }
        #endregion
    }
}
