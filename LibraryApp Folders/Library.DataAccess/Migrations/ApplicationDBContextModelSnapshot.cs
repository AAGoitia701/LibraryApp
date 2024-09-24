﻿// <auto-generated />
using Library.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.Models.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "SciFi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Library.Models.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("ListPrice30")
                        .HasColumnType("float");

                    b.Property<double>("ListPriceHigher30")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "J.K Rowling",
                            CategoryId = 2,
                            Description = "The hope and wonder of Harry Potter's world will make you want to escape to Hogwarts again and again. The magic starts here!",
                            ISBN = "1408855658",
                            ImageURL = "",
                            ListPrice = 11.9,
                            ListPrice30 = 7.5300000000000002,
                            ListPriceHigher30 = 5.5499999999999998,
                            Title = "Harry Potter And The Philosopher'S Stone"
                        },
                        new
                        {
                            Id = 2,
                            Author = "J.R.R Tolkien",
                            CategoryId = 2,
                            Description = "It is the ancient drama to which the characters in The Lord of the Rings look back, and in whose events some of them such as Elrond and Galadriel took part.",
                            ISBN = "9780261102736",
                            ImageURL = "",
                            ListPrice = 8.0,
                            ListPrice30 = 6.2300000000000004,
                            ListPriceHigher30 = 4.6500000000000004,
                            Title = "The Silmarillion"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Isaac Asimov",
                            CategoryId = 2,
                            Description = "In these stories Isaac Asimov creates the Three Laws of Robotics and ushers in the Robot Age.\r\n\r\nEarth is ruled by master-machines but the Three Laws of Robotics have been designed to ensure humans maintain the upper hand.",
                            ISBN = "9780007532278",
                            ImageURL = "",
                            ListPrice = 10.0,
                            ListPrice30 = 7.4299999999999997,
                            ListPriceHigher30 = 5.25,
                            Title = "I, Robot"
                        });
                });

            modelBuilder.Entity("Library.Models.Models.Product", b =>
                {
                    b.HasOne("Library.Models.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
