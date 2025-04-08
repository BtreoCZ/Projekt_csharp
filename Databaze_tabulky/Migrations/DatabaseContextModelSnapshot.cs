﻿// <auto-generated />
using System;
using Databaze_tabulky;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Databaze_tabulky.Migrations
{
    [DbContext(typeof(MyORM))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("Databaze_tabulky.Highschool", b =>
                {
                    b.Property<int>("HighSchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("HighSchoolId");

                    b.ToTable("Highschools");
                });

            modelBuilder.Entity("Databaze_tabulky.HighschoolApplication", b =>
                {
                    b.Property<int>("HighschoolApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HighschoolApplicationId");

                    b.ToTable("HighschoolApplications");
                });

            modelBuilder.Entity("Databaze_tabulky.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DatumNarozeni")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Jmeno")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mesto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PSC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prijmeni")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RodneCislo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Databaze_tabulky.StudyField", b =>
                {
                    b.Property<int>("StudyFieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Graduation")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HighSchoolId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudyDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyFieldCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudySlots")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudyFieldId");

                    b.ToTable("StudyFields");
                });

            modelBuilder.Entity("Databaze_tabulky.StudyFieldApplication", b =>
                {
                    b.Property<int>("StudyFieldApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HighschoolApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudyFieldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudyFieldApplicationId");

                    b.ToTable("StudyFieldApplications");
                });
#pragma warning restore 612, 618
        }
    }
}
