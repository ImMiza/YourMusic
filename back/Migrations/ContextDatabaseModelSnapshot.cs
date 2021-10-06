﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using yourmusic.Context;

namespace back.Migrations
{
    [DbContext(typeof(ContextDatabase))]
    partial class ContextDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("yourmusic.Models.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RealeaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlMusic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("yourmusic.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RealeaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImage")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Playlists");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Playlist");
                });

            modelBuilder.Entity("yourmusic.Models.Album", b =>
                {
                    b.HasBaseType("yourmusic.Models.Playlist");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Album");
                });

            modelBuilder.Entity("yourmusic.Models.Music", b =>
                {
                    b.HasOne("yourmusic.Models.Playlist", null)
                        .WithMany("Musics")
                        .HasForeignKey("PlaylistId");
                });

            modelBuilder.Entity("yourmusic.Models.Playlist", b =>
                {
                    b.Navigation("Musics");
                });
#pragma warning restore 612, 618
        }
    }
}
