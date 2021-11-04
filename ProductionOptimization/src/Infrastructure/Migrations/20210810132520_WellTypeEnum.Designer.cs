﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210810132520_WellTypeEnum")]
    partial class WellTypeEnum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.ModelComponents.IPR", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GasFractionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiftTableContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiftTablePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pressures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductivityIndexId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Rates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReservoirPressureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReservoirTemperatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SystemAnalysisModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("UseLiftTable")
                        .HasColumnType("bit");

                    b.Property<Guid?>("WaterFractionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GasFractionId");

                    b.HasIndex("ProductivityIndexId");

                    b.HasIndex("ReservoirPressureId");

                    b.HasIndex("ReservoirTemperatureId");

                    b.HasIndex("SystemAnalysisModelId")
                        .IsUnique();

                    b.HasIndex("WaterFractionId");

                    b.ToTable("IPR");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.ModelBackground", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ArtificialLift")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlowType")
                        .HasColumnType("int");

                    b.Property<int>("FluidType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SandControl")
                        .HasColumnType("bit");

                    b.Property<bool>("SurfaceProfileModelling")
                        .HasColumnType("bit");

                    b.Property<Guid>("SystemAnalysisModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TemperatureModelling")
                        .HasColumnType("bit");

                    b.Property<bool>("UseLiftTable")
                        .HasColumnType("bit");

                    b.Property<int>("WellType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemAnalysisModelId")
                        .IsUnique();

                    b.ToTable("ModelBackgrounds");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.PVT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlackOilModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("C02")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FluidType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GasGravity")
                        .HasColumnType("float");

                    b.Property<Guid?>("GasRatioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("GasViscosity")
                        .HasColumnType("float");

                    b.Property<double>("H2S")
                        .HasColumnType("float");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("N2")
                        .HasColumnType("float");

                    b.Property<double>("OilGravity")
                        .HasColumnType("float");

                    b.Property<Guid?>("PressureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RSBO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SystemAnalysisID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TemperatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("WaterSalinityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GasRatioId");

                    b.HasIndex("PressureId");

                    b.HasIndex("SystemAnalysisID")
                        .IsUnique();

                    b.HasIndex("TemperatureId");

                    b.HasIndex("WaterSalinityId");

                    b.ToTable("PVTs");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.VLP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GasFractionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GasLiftFractionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiftTableContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiftTablePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pressures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SystemAnalysisModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("THPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("UseLiftTable")
                        .HasColumnType("bit");

                    b.Property<Guid?>("WaterFractionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GasFractionId");

                    b.HasIndex("GasLiftFractionId");

                    b.HasIndex("SystemAnalysisModelId")
                        .IsUnique();

                    b.HasIndex("THPId");

                    b.HasIndex("WaterFractionId");

                    b.ToTable("VLP");
                });

            modelBuilder.Entity("Domain.Entities.ParamEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ParamEntries");
                });

            modelBuilder.Entity("Domain.Entities.SystemAnalysisModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrainagePointName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FluidPropertyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModelDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemAnalysisModels");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.IPR", b =>
                {
                    b.HasOne("Domain.Entities.ParamEntry", "GasFraction")
                        .WithMany()
                        .HasForeignKey("GasFractionId");

                    b.HasOne("Domain.Entities.ParamEntry", "ProductivityIndex")
                        .WithMany()
                        .HasForeignKey("ProductivityIndexId");

                    b.HasOne("Domain.Entities.ParamEntry", "ReservoirPressure")
                        .WithMany()
                        .HasForeignKey("ReservoirPressureId");

                    b.HasOne("Domain.Entities.ParamEntry", "ReservoirTemperature")
                        .WithMany()
                        .HasForeignKey("ReservoirTemperatureId");

                    b.HasOne("Domain.Entities.SystemAnalysisModel", "SystemAnalysisEOIModel")
                        .WithOne("IPR")
                        .HasForeignKey("Domain.Entities.ModelComponents.IPR", "SystemAnalysisModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ParamEntry", "WaterFraction")
                        .WithMany()
                        .HasForeignKey("WaterFractionId");

                    b.Navigation("GasFraction");

                    b.Navigation("ProductivityIndex");

                    b.Navigation("ReservoirPressure");

                    b.Navigation("ReservoirTemperature");

                    b.Navigation("SystemAnalysisEOIModel");

                    b.Navigation("WaterFraction");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.ModelBackground", b =>
                {
                    b.HasOne("Domain.Entities.SystemAnalysisModel", "SystemAnalysisEOIModel")
                        .WithOne("ModelBackground")
                        .HasForeignKey("Domain.Entities.ModelComponents.ModelBackground", "SystemAnalysisModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SystemAnalysisEOIModel");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.PVT", b =>
                {
                    b.HasOne("Domain.Entities.ParamEntry", "GasRatio")
                        .WithMany()
                        .HasForeignKey("GasRatioId");

                    b.HasOne("Domain.Entities.ParamEntry", "Pressure")
                        .WithMany()
                        .HasForeignKey("PressureId");

                    b.HasOne("Domain.Entities.SystemAnalysisModel", "SystemAnalysisModel")
                        .WithOne("PVT")
                        .HasForeignKey("Domain.Entities.ModelComponents.PVT", "SystemAnalysisID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ParamEntry", "Temperature")
                        .WithMany()
                        .HasForeignKey("TemperatureId");

                    b.HasOne("Domain.Entities.ParamEntry", "WaterSalinity")
                        .WithMany()
                        .HasForeignKey("WaterSalinityId");

                    b.Navigation("GasRatio");

                    b.Navigation("Pressure");

                    b.Navigation("SystemAnalysisModel");

                    b.Navigation("Temperature");

                    b.Navigation("WaterSalinity");
                });

            modelBuilder.Entity("Domain.Entities.ModelComponents.VLP", b =>
                {
                    b.HasOne("Domain.Entities.ParamEntry", "GasFraction")
                        .WithMany()
                        .HasForeignKey("GasFractionId");

                    b.HasOne("Domain.Entities.ParamEntry", "GasLiftFraction")
                        .WithMany()
                        .HasForeignKey("GasLiftFractionId");

                    b.HasOne("Domain.Entities.SystemAnalysisModel", "SystemAnalysisEOIModel")
                        .WithOne("VLP")
                        .HasForeignKey("Domain.Entities.ModelComponents.VLP", "SystemAnalysisModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ParamEntry", "THP")
                        .WithMany()
                        .HasForeignKey("THPId");

                    b.HasOne("Domain.Entities.ParamEntry", "WaterFraction")
                        .WithMany()
                        .HasForeignKey("WaterFractionId");

                    b.Navigation("GasFraction");

                    b.Navigation("GasLiftFraction");

                    b.Navigation("SystemAnalysisEOIModel");

                    b.Navigation("THP");

                    b.Navigation("WaterFraction");
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
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
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

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.SystemAnalysisModel", b =>
                {
                    b.Navigation("IPR");

                    b.Navigation("ModelBackground");

                    b.Navigation("PVT");

                    b.Navigation("VLP");
                });
#pragma warning restore 612, 618
        }
    }
}
