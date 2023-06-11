﻿// <auto-generated />
using System;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalPaper.Infrastructure.Migrations
{
    [DbContext(typeof(FinalPaperDBContext))]
    [Migration("20230611204830_UpdatedThesisDefenceConfig")]
    partial class UpdatedThesisDefenceConfig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalPaper.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MentorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CourseTypeId");

                    b.HasIndex("MentorId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RevokedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.Thesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ThesisStatusTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Thesis");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.ThesisDefence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DefenceScore")
                        .HasColumnType("int");

                    b.Property<int?>("FinalPaperScore")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Room")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThesesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThesesId");

                    b.ToTable("ThesisDefence");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.ThesisDefenceUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ThesisDefenceId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "ThesisDefenceId");

                    b.HasIndex("ThesisDefenceId");

                    b.ToTable("ThesisDefenceUser");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModifiedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinalPaper.Domain.Enums.CourseTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Preddiplomski"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Diplomski"
                        });
                });

            modelBuilder.Entity("FinalPaper.Domain.Enums.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mentor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Student"
                        });
                });

            modelBuilder.Entity("FinalPaper.Domain.Enums.ThesisStatusTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ThesisStatusTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Čeka odobrenje"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Odobreno"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Odbijeno"
                        });
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.Course", b =>
                {
                    b.HasOne("FinalPaper.Domain.Enums.CourseTypes", "CourseType")
                        .WithMany()
                        .HasForeignKey("CourseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalPaper.Domain.Entities.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("FinalPaper.Domain.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("FinalPaper.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.Thesis", b =>
                {
                    b.HasOne("FinalPaper.Domain.Entities.Course", "Course")
                        .WithMany("Theses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalPaper.Domain.Entities.User", "User")
                        .WithMany("Theses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.ThesisDefence", b =>
                {
                    b.HasOne("FinalPaper.Domain.Entities.Thesis", "Thesis")
                        .WithMany("ThesisDefences")
                        .HasForeignKey("ThesesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.ThesisDefenceUser", b =>
                {
                    b.HasOne("FinalPaper.Domain.Entities.ThesisDefence", "ThesisDefence")
                        .WithMany("ThesesDefenceUsers")
                        .HasForeignKey("ThesisDefenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalPaper.Domain.Entities.User", "User")
                        .WithMany("ThesesDefenceUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThesisDefence");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.User", b =>
                {
                    b.HasOne("FinalPaper.Domain.Enums.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.Course", b =>
                {
                    b.Navigation("Theses");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.Thesis", b =>
                {
                    b.Navigation("ThesisDefences");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.ThesisDefence", b =>
                {
                    b.Navigation("ThesesDefenceUsers");
                });

            modelBuilder.Entity("FinalPaper.Domain.Entities.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("RefreshToken");

                    b.Navigation("Theses");

                    b.Navigation("ThesesDefenceUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
