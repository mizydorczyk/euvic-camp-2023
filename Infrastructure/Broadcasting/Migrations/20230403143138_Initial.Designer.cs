﻿// <auto-generated />
using System;
using Infrastructure.Broadcasting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Broadcasting.Migrations
{
    [DbContext(typeof(BroadcastingDbContext))]
    [Migration("20230403143138_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KnownAs")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Core.Entities.Piece", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Version")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<long>("Views")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("Core.Entities.ProgrammeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("BroadcastingDuration")
                        .HasColumnType("interval");

                    b.Property<int>("PieceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PlaybackDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RadioChannelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PieceId");

                    b.HasIndex("RadioChannelId");

                    b.ToTable("ProgrammeItems");
                });

            modelBuilder.Entity("Core.Entities.RadioChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Frequency")
                        .HasColumnType("decimal(4, 1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("RadioChannels");
                });

            modelBuilder.Entity("Core.Entities.Piece", b =>
                {
                    b.HasOne("Core.Entities.Artist", "Artist")
                        .WithMany("Pieces")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Core.Entities.ProgrammeItem", b =>
                {
                    b.HasOne("Core.Entities.Piece", "Piece")
                        .WithMany("ProgrammesItems")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.RadioChannel", "RadioChannel")
                        .WithMany("ProgrammeItems")
                        .HasForeignKey("RadioChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");

                    b.Navigation("RadioChannel");
                });

            modelBuilder.Entity("Core.Entities.Artist", b =>
                {
                    b.Navigation("Pieces");
                });

            modelBuilder.Entity("Core.Entities.Piece", b =>
                {
                    b.Navigation("ProgrammesItems");
                });

            modelBuilder.Entity("Core.Entities.RadioChannel", b =>
                {
                    b.Navigation("ProgrammeItems");
                });
#pragma warning restore 612, 618
        }
    }
}