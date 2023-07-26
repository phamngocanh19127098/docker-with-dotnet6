using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.Models
{
    public partial class TicketOrderContext : DbContext
    {
        public TicketOrderContext()
        {
        }

        public TicketOrderContext(DbContextOptions<TicketOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<TicketInformation> TicketInformations { get; set; } = null!;
        public virtual DbSet<TicketOrder> TicketOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=docker.host.internal,1422;Database=TicketOrder;User Id=sa;Password=trungdb123@@;TrustServerCertificate=True", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK_Order_Passenger");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("Passenger");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdentityCard).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(12);
            });

            modelBuilder.Entity<TicketInformation>(entity =>
            {
                entity.ToTable("TicketInformation");

                entity.Property(e => e.Arrival).HasMaxLength(50);

                entity.Property(e => e.Departure).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TicketInformations)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Ticket_Class");
            });

            modelBuilder.Entity<TicketOrder>(entity =>
            {
                entity.ToTable("TicketOrder");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TicketOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_TicketOrder_Order");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketOrders)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_TicketOrder_Ticket");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
