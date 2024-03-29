﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.Models
{
    public partial class BusTourContext : DbContext
    {
        public BusTourContext()
        {
        }

        public BusTourContext(DbContextOptions<BusTourContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Journey> Journeys { get; set; } = null!;
        public virtual DbSet<Medium> Media { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Surcharge> Surcharges { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<TourPlace> TourPlaces { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=docker.host.internal,1411;Database=BusTour;User Id=sa;Password=trungdb123@@;TrustServerCertificate=True", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.ToTable("Journey");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.Property(e => e.Blog).HasMaxLength(100);

                entity.Property(e => e.Language).HasMaxLength(20);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.TimeStay).HasColumnType("datetime");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("FK_Place_Media");
            });

            modelBuilder.Entity<Surcharge>(entity =>
            {
                entity.ToTable("Surcharge");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Arrival).HasMaxLength(50);

                entity.Property(e => e.Departure).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Ticket_Class");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Ticket_Tour");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK_Tour_Journey");

                entity.HasOne(d => d.Surcharge)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.SurchargeId)
                    .HasConstraintName("FK_Tour_Surcharge");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Tour_Vehicle");
            });

            modelBuilder.Entity<TourPlace>(entity =>
            {
                entity.ToTable("TourPlace");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.TourPlaces)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK_TourPlace_Journey");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.TourPlaces)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_TourPlace_Place");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
