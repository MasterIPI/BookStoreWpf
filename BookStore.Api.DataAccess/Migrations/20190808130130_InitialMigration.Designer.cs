﻿// <auto-generated />
using System;
using BookStore.Api.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookStore.Api.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190808130130_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookStore.Api.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("BookStore.Api.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgeRating");

                    b.Property<int>("CoverType");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<bool>("HasDustJacket");

                    b.Property<int>("PageNumber");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 8)");

                    b.Property<int>("ProductType");

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("QuantityAvailable");

                    b.Property<float>("Rating");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BookStore.Api.Entities.ProductAuthor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("ProductId");

                    b.HasKey("Id");

                    b.ToTable("ProductAuthor");
                });

            modelBuilder.Entity("BookStore.Api.Entities.ProductPublisher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("ProductId");

                    b.Property<long>("PublisherId");

                    b.HasKey("Id");

                    b.ToTable("ProductPublisher");
                });

            modelBuilder.Entity("BookStore.Api.Entities.Publisher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("BookStore.Api.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.Property<string>("UserName");

                    b.Property<long>("UserRoleId");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BookStore.Api.Entities.UserBasket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserBasket");
                });

            modelBuilder.Entity("BookStore.Api.Entities.UserBasketProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("ProductId");

                    b.Property<long>("UserBasketId");

                    b.HasKey("Id");

                    b.ToTable("UserBasketProduct");
                });

            modelBuilder.Entity("BookStore.Api.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreationDate = new DateTime(2019, 8, 8, 13, 1, 30, 48, DateTimeKind.Utc).AddTicks(2568),
                            Role = 0
                        },
                        new
                        {
                            Id = 2L,
                            CreationDate = new DateTime(2019, 8, 8, 13, 1, 30, 48, DateTimeKind.Utc).AddTicks(3860),
                            Role = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
