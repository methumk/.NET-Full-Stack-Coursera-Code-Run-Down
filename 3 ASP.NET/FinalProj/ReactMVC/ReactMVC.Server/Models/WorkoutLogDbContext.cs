using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReactMVC.Server.Models;

public partial class WorkoutLogDbContext : DbContext
{
    public WorkoutLogDbContext()
    {
    }

    public WorkoutLogDbContext(DbContextOptions<WorkoutLogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserModel> Users { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

// NOTE: removing to try and work with DI in Program.cs
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=.;Database=CheckDB;Integrated Security=true;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A4689340");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FF6BA5A6").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347AEA612E").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workouts__3214EC07C3CC4CC7");

            entity.Property(e => e.Exercise)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Workouts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workouts__User_i__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
