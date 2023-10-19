﻿// <auto-generated />
using System;
using System.Collections.Generic;
using GuessNumber.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GuessNumber.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GuessNumber.Entity.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Attempts")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Description")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool>("Win")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GuessNumber.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GuessNumber.Entity.Game", b =>
                {
                    b.HasOne("GuessNumber.Entity.User", null)
                        .WithMany("Games")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GuessNumber.Entity.User", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
