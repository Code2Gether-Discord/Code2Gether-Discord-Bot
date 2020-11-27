﻿// <auto-generated />
using System;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Code2Gether_Discord_Bot.WebApi.Migrations
{
    [DbContext(typeof(DiscordBotDbContext))]
    [Migration("20201126160034_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.Project", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AuthorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.ProjectRole", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanReadData")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanWriteData")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RoleName")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.User", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.Project", b =>
                {
                    b.HasOne("Code2Gether_Discord_Bot.Library.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.User", b =>
                {
                    b.HasOne("Code2Gether_Discord_Bot.Library.Models.Project", null)
                        .WithMany("ProjectMembers")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("Code2Gether_Discord_Bot.Library.Models.Project", b =>
                {
                    b.Navigation("ProjectMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
