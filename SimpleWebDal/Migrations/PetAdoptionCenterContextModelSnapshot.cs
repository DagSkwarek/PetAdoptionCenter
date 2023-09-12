﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleWebDal.Data;

#nullable disable

namespace SimpleWebDal.Migrations
{
    [DbContext(typeof(PetAdoptionCenterContext))]
    partial class PetAdoptionCenterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<int>("FlatNumber")
                        .HasColumnType("integer");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            Id = 2L,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            Id = 3L,
                            City = "Warsaw",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        },
                        new
                        {
                            Id = 4L,
                            City = "Gdynia",
                            FlatNumber = 3,
                            HouseNumber = "3a",
                            PostalCode = "48-456",
                            Street = "Janasa"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
