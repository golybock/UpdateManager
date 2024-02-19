using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UM.API.Data;

public partial class UmFullContext : DbContext
{
    public UmFullContext()
    {
    }

    public UmFullContext(DbContextOptions<UmFullContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dependency> Dependencies { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    public virtual DbSet<VersionDependency> VersionDependencies { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5432;username=admin;password=admin;database=um_full");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dependency_pkey");

            entity.ToTable("dependency");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("task_pkey");

            entity.ToTable("task");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientEmail).HasColumnName("client_email");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_time");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Solution).HasColumnName("solution");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Worker).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("task_worker_id_fkey");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("version_pkey");

            entity.ToTable("version");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Available)
                .HasDefaultValue(false)
                .HasColumnName("available");
            entity.Property(e => e.Build).HasColumnName("build");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("now()")
                .HasColumnName("timestamp");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<VersionDependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("version_dependencies_pkey");

            entity.ToTable("version_dependencies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DependencyId).HasColumnName("dependency_id");
            entity.Property(e => e.VersionId).HasColumnName("version_id");

            entity.HasOne(d => d.Dependency).WithMany(p => p.VersionDependencies)
                .HasForeignKey(d => d.DependencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("version_dependencies_dependency_id_fkey");

            entity.HasOne(d => d.Version).WithMany(p => p.VersionDependencies)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("version_dependencies_version_id_fkey");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pkey");

            entity.ToTable("worker");

            entity.HasIndex(e => e.Login, "worker_login_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .HasColumnName("full_name");
            entity.Property(e => e.Login)
                .HasMaxLength(250)
                .HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
