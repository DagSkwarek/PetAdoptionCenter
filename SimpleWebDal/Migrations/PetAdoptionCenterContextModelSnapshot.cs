﻿// <auto-generated />
using System;
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
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SimpleWebDal.Models.AdoptionProccess.Adoption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("ContractAdoption")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DateOfAdoption")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Meetings")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uuid");

                    b.Property<bool>("PreAdoptionPoll")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ShelterId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ShelterId");

                    b.HasIndex("UserId");

                    b.ToTable("Adoptions");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.BasicHealthInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BasicHealthInfos");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Disease", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BasicHealthInfoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("IllnessEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("IllnessStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NameOfdisease")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BasicHealthInfoId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("AvaibleForAdoption")
                        .HasColumnType("boolean");

                    b.Property<Guid>("BasicHealthInfoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ShelterId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TempHouseId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BasicHealthInfoId")
                        .IsUnique();

                    b.HasIndex("CalendarId")
                        .IsUnique();

                    b.HasIndex("ShelterId");

                    b.HasIndex("TempHouseId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Vaccination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BasicHealthInfoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VaccinationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BasicHealthInfoId");

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ActivityDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CalendarActivityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CalendarActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.CalendarActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("CalendarActivities");
                });

            modelBuilder.Entity("SimpleWebDal.Models.PetShelter.Shelter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShelterDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CalendarId")
                        .IsUnique();

                    b.ToTable("Shelters");
                });

            modelBuilder.Entity("SimpleWebDal.Models.TemporaryHouse.TempHouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ShelterId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("StartOfTemporaryHouseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ShelterId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TempHouses");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("FlatNumber")
                        .HasColumnType("integer");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.BasicInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("BasicInformations");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Title")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("BasicInformationId")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<Guid?>("ShelterId")
                        .HasColumnType("uuid");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("UserCalendarId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("BasicInformationId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ShelterId");

                    b.HasIndex("UserCalendarId")
                        .IsUnique();

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UserPetsJoinTable", b =>
                {
                    b.Property<Guid>("PetsId")
                        .HasColumnType("uuid");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("PetsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserPetsJoinTable");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleWebDal.Models.AdoptionProccess.Adoption", b =>
                {
                    b.HasOne("SimpleWebDal.Models.PetShelter.Shelter", null)
                        .WithMany("Adoptions")
                        .HasForeignKey("ShelterId");

                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany("Adoptions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Disease", b =>
                {
                    b.HasOne("SimpleWebDal.Models.Animal.BasicHealthInfo", null)
                        .WithMany("MedicalHistory")
                        .HasForeignKey("BasicHealthInfoId");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Pet", b =>
                {
                    b.HasOne("SimpleWebDal.Models.Animal.BasicHealthInfo", "BasicHealthInfo")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.Animal.Pet", "BasicHealthInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", "Calendar")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.Animal.Pet", "CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.PetShelter.Shelter", null)
                        .WithMany("ShelterPets")
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.TemporaryHouse.TempHouse", null)
                        .WithMany("PetsInTemporaryHouse")
                        .HasForeignKey("TempHouseId");

                    b.Navigation("BasicHealthInfo");

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.Vaccination", b =>
                {
                    b.HasOne("SimpleWebDal.Models.Animal.BasicHealthInfo", null)
                        .WithMany("Vaccinations")
                        .HasForeignKey("BasicHealthInfoId");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.Activity", b =>
                {
                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", null)
                        .WithMany("Activities")
                        .HasForeignKey("CalendarActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleWebDal.Models.PetShelter.Shelter", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.Address", "ShelterAddress")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.PetShelter.Shelter", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", "ShelterCalendar")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.PetShelter.Shelter", "CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShelterAddress");

                    b.Navigation("ShelterCalendar");
                });

            modelBuilder.Entity("SimpleWebDal.Models.TemporaryHouse.TempHouse", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.Address", "TemporaryHouseAddress")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.TemporaryHouse.TempHouse", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.PetShelter.Shelter", null)
                        .WithMany("TempHouses")
                        .HasForeignKey("ShelterId");

                    b.HasOne("SimpleWebDal.Models.WebUser.User", "TemporaryOwner")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.TemporaryHouse.TempHouse", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TemporaryHouseAddress");

                    b.Navigation("TemporaryOwner");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.BasicInformation", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.Address", "Address")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.WebUser.BasicInformation", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.Role", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.User", b =>
                {
                    b.HasOne("SimpleWebDal.Models.WebUser.BasicInformation", "BasicInformation")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.WebUser.User", "BasicInformationId");

                    b.HasOne("SimpleWebDal.Models.PetShelter.Shelter", null)
                        .WithMany("ShelterUsers")
                        .HasForeignKey("ShelterId");

                    b.HasOne("SimpleWebDal.Models.CalendarModel.CalendarActivity", "UserCalendar")
                        .WithOne()
                        .HasForeignKey("SimpleWebDal.Models.WebUser.User", "UserCalendarId");

                    b.Navigation("BasicInformation");

                    b.Navigation("UserCalendar");
                });

            modelBuilder.Entity("UserPetsJoinTable", b =>
                {
                    b.HasOne("SimpleWebDal.Models.Animal.Pet", null)
                        .WithMany()
                        .HasForeignKey("PetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleWebDal.Models.WebUser.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleWebDal.Models.Animal.BasicHealthInfo", b =>
                {
                    b.Navigation("MedicalHistory");

                    b.Navigation("Vaccinations");
                });

            modelBuilder.Entity("SimpleWebDal.Models.CalendarModel.CalendarActivity", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("SimpleWebDal.Models.PetShelter.Shelter", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("ShelterPets");

                    b.Navigation("ShelterUsers");

                    b.Navigation("TempHouses");
                });

            modelBuilder.Entity("SimpleWebDal.Models.TemporaryHouse.TempHouse", b =>
                {
                    b.Navigation("PetsInTemporaryHouse");
                });

            modelBuilder.Entity("SimpleWebDal.Models.WebUser.User", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
