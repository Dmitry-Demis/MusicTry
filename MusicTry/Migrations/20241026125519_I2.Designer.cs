﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicTry;

#nullable disable

namespace MusicTry.Migrations
{
    [DbContext(typeof(MusicContext))]
    [Migration("20241026125519_I2")]
    partial class I2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("MusicTry.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicTry.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicTry.Compilation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Compilations");
                });

            modelBuilder.Entity("MusicTry.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MusicTry.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompilationId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("CompilationId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("MusicTry.Album", b =>
                {
                    b.HasOne("MusicTry.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicTry.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MusicTry.Track", b =>
                {
                    b.HasOne("MusicTry.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicTry.Compilation", null)
                        .WithMany("Tracks")
                        .HasForeignKey("CompilationId");

                    b.Navigation("Album");
                });

            modelBuilder.Entity("MusicTry.Album", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("MusicTry.Artist", b =>
                {
                    b.Navigation("Albums");
                });

            modelBuilder.Entity("MusicTry.Compilation", b =>
                {
                    b.Navigation("Tracks");
                });
#pragma warning restore 612, 618
        }
    }
}