﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectWork.Data;

namespace ProjectWork.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211029151117_Paperino")]
    partial class Paperino
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ProjectWork.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("UserSsn")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("UserSsn");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ProjectWork.Models.User", b =>
                {
                    b.Property<string>("Ssn")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<byte[]>("HashedPassword")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Pob")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Ssn");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectWork.Models.Post", b =>
                {
                    b.HasOne("ProjectWork.Models.User", null)
                        .WithMany("Posts")
                        .HasForeignKey("UserSsn");
                });

            modelBuilder.Entity("ProjectWork.Models.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
