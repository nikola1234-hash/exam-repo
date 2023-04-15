﻿// <auto-generated />
using System;
using Server.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(ServerDbContext))]
    partial class ServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GradeEntityId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WrongAnswerSelected")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradeEntityId");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LecturerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RandomizeQuestions")
                        .HasColumnType("bit");

                    b.Property<long>("TickCount")
                        .HasColumnType("bigint");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.GradeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<double>("Grade")
                        .HasColumnType("float");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RandomizeAnswers")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "123",
                            Username = "Lector"
                        },
                        new
                        {
                            Id = 2,
                            Password = "123",
                            Username = "Student"
                        });
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Answer", b =>
                {
                    b.HasOne("ExamServer.EntityFramework.Entities.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Error", b =>
                {
                    b.HasOne("ExamServer.EntityFramework.Entities.GradeEntity", null)
                        .WithMany("Errors")
                        .HasForeignKey("GradeEntityId");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.GradeEntity", b =>
                {
                    b.HasOne("ExamServer.EntityFramework.Entities.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Question", b =>
                {
                    b.HasOne("ExamServer.EntityFramework.Entities.Exam", null)
                        .WithMany("Questions")
                        .HasForeignKey("ExamId");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Exam", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.GradeEntity", b =>
                {
                    b.Navigation("Errors");
                });

            modelBuilder.Entity("ExamServer.EntityFramework.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
