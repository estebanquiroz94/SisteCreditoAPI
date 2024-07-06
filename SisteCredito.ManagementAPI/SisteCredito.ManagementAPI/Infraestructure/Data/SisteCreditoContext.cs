using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Infraestructure.Data
{
    public partial class SisteCreditoContext : DbContext
    {
        public SisteCreditoContext()
        {
        }

        public SisteCreditoContext(DbContextOptions<SisteCreditoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Charge> Charges { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ExtraHour> ExtraHours { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-JUANQUIR\\SQLEXPRESS;Database=SisteCredito; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.HasIndex(e => e.Code, "UQ__Area__A25C5AA7151548F9")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdHumangestorNavigation)
                    .WithMany(p => p.AreaIdHumangestorNavigations)
                    .HasForeignKey(d => d.IdHumangestor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HumanGestor");

                entity.HasOne(d => d.IdSupervisorNavigation)
                    .WithMany(p => p.AreaIdSupervisorNavigations)
                    .HasForeignKey(d => d.IdSupervisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supervidor");
            });

            modelBuilder.Entity<Charge>(entity =>
            {
                entity.ToTable("Charge");

                entity.HasIndex(e => e.Code, "UQ__Charge__A25C5AA72877D540")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359E8A6C1EE3")
                    .IsUnique();

                entity.Property(e => e.Document)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdChargeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdCharge)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Charge");

                entity.HasOne(d => d.IdSupervisorNavigation)
                    .WithMany(p => p.InverseIdSupervisorNavigation)
                    .HasForeignKey(d => d.IdSupervisor)
                    .HasConstraintName("FK_Supervisor");
            });

            modelBuilder.Entity<ExtraHour>(entity =>
            {
                entity.ToTable("ExtraHour");

                entity.Property(e => e.DateRegister).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
