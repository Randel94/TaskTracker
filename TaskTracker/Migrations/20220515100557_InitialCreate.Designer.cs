﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskTracker.Data;

#nullable disable

namespace TaskTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220515100557_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaskTracker.Models.TaskModel", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<decimal>("CompletedTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DateFinish")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReg")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<decimal>("EstimatedTime")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Executor")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("TaskId");

                    b.HasIndex("ParentId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("TaskTracker.Models.TaskModel", b =>
                {
                    b.HasOne("TaskTracker.Models.TaskModel", "ParentTask")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentTask");
                });
#pragma warning restore 612, 618
        }
    }
}