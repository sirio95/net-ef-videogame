﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using net_ef_videogame;

#nullable disable

namespace net_ef_videogame.Migrations
{
    [DbContext(typeof(Program.VideoGames))]
    partial class VideoGamesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("net_ef_videogame.Program+Software_House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<string>("VATnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("vat_num");

                    b.HasKey("Id");

                    b.ToTable("Software_Houses");
                });

            modelBuilder.Entity("net_ef_videogame.Program+VideoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("overview");

                    b.Property<DateTime>("Release")
                        .HasColumnType("datetime2")
                        .HasColumnName("release_date");

                    b.Property<int>("Software_HouseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Software_HouseId");

                    b.ToTable("Videogames");
                });

            modelBuilder.Entity("net_ef_videogame.Program+VideoGame", b =>
                {
                    b.HasOne("net_ef_videogame.Program+Software_House", "Software_House")
                        .WithMany("Videogames")
                        .HasForeignKey("Software_HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software_House");
                });

            modelBuilder.Entity("net_ef_videogame.Program+Software_House", b =>
                {
                    b.Navigation("Videogames");
                });
#pragma warning restore 612, 618
        }
    }
}
