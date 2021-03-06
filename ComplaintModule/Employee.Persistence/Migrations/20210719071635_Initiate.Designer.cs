// <auto-generated />
using System;
using Employee.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Employee.Persistence.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20210719071635_Initiate")]
    partial class Initiate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("User.Core.Entity.ComplaintEntity", b =>
                {
                    b.Property<byte[]>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Complaint")
                        .HasColumnType("text");

                    b.Property<int>("ReporterId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ComplaintId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("User.Core.Entity.SecurityEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Security");
                });

            modelBuilder.Entity("User.Core.Entity.UserEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DEPARTMENT")
                        .HasColumnType("text");

                    b.Property<string>("DESIGNATION")
                        .HasColumnType("text");

                    b.Property<string>("EMAIL")
                        .HasColumnType("text");

                    b.Property<string>("NAME")
                        .HasColumnType("text");

                    b.Property<string>("TYPE")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
