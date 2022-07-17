using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WpfApp4.Models;
namespace WpfApp4
{
    public partial class UserTaskBdContext : DbContext
    {
        public UserTaskBdContext()
        {
            
        }

        public UserTaskBdContext(DbContextOptions<UserTaskBdContext> options)
            : base(options)
        {
        }

        internal virtual DbSet<Status> Statuses { get; set; } = null!;
        internal virtual DbSet<Task> Tasks { get; set; } = null!;
        internal virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserTaskBd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.CurrentStatus).HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.DateOfPublication).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Status");

                entity.HasOne(d => d.TaskMaker)
                    .WithMany(p => p.TaskTaskMakers)
                    .HasForeignKey(d => d.TaskMakerId)
                    .HasConstraintName("FK_Task_Users1");

                entity.HasOne(d => d.Tasker)
                    .WithMany(p => p.TaskTaskers)
                    .HasForeignKey(d => d.TaskerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NumberPhone).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
