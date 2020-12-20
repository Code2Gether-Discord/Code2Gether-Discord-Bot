﻿// <auto-generated />
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    [DbContext(typeof(DiscordBotDbContext))]
    [Migration("20201220152420_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("MEMBER_ID");

                    b.Property<ulong>("SnowflakeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MEMBER_SNOWFLAKE_ID");

                    b.HasKey("ID");

                    b.ToTable("MEMBERS");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PROJECT_ID");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("AUTHOR_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PROJECT_NAME");

                    b.HasKey("ID");

                    b.ToTable("PROJECTS");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.ProjectMember", b =>
                {
                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MEMBER_ID");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("PROJECT_ID");

                    b.HasKey("MemberID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.ToTable("PROJECT_MEMBER");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.ProjectMember", b =>
                {
                    b.HasOne("Code2Gether_Discord_Bot.Library.Models.Member", null)
                        .WithMany()
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code2Gether_Discord_Bot.Library.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}