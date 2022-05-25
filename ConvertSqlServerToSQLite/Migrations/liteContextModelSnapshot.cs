﻿// <auto-generated />
using ConvertSqlServerToSQLite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConvertSqlServerToSQLite.Migrations
{
    [DbContext(typeof(liteContext))]
    partial class liteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("ConvertSqlServerToSQLite.Models.Personne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("country")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.Property<int>("Old")
                        .HasColumnType("INTEGER")
                        .HasColumnName("old");

                    b.HasKey("Id");

                    b.ToTable("personne", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
