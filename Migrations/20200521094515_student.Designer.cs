﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Table.Date;

namespace Table.Migrations
{
    [DbContext(typeof(TableContext))]
    [Migration("20200521094515_student")]
    partial class student
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Table.Models.RowType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rus_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Table_id")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("RowTypes");
                });

            modelBuilder.Entity("Table.Models.Schedule", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id_cours")
                        .HasColumnType("int");

                    b.Property<int>("id_group")
                        .HasColumnType("int");

                    b.Property<int>("id_teacher")
                        .HasColumnType("int");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("stop")
                        .HasColumnType("datetime2");

                    b.Property<string>("teacher")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Table.Models.Tables.Cours", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("count_in_week")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Table.Models.Tables.Group", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("id_cours")
                        .HasColumnType("int");

                    b.Property<int>("id_teacher")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Table.Models.Tables.InfoTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rus_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InfoTables");
                });

            modelBuilder.Entity("Table.Models.Tables.People", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Family_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fathers_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Peoples");

                    b.HasDiscriminator<string>("Discriminator").HasValue("People");
                });

            modelBuilder.Entity("Table.Models.Tables.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CoursId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CoursId");

                    b.HasIndex("CoursId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("Table.Models.Tables.Student", b =>
                {
                    b.HasBaseType("Table.Models.Tables.People");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Table.Models.Tables.Teacher", b =>
                {
                    b.HasBaseType("Table.Models.Tables.People");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Table.Models.Tables.StudentCourse", b =>
                {
                    b.HasOne("Table.Models.Tables.Cours", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Table.Models.Tables.Student", "Student")
                        .WithMany("Students")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
