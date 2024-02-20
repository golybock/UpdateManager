using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimeTableSite.Data;

public partial class UmFullContext : DbContext
{
    public UmFullContext()
    {
    }

    public UmFullContext(DbContextOptions<UmFullContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allowance> Allowances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependency> Dependencies { get; set; }

    public virtual DbSet<Period> Periods { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<TimetableRole> TimetableRoles { get; set; }

    public virtual DbSet<TimetableRoleAllowance> TimetableRoleAllowances { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    public virtual DbSet<VersionDependency> VersionDependencies { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5432;username=admin;password=admin;database=um_full;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allowance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("allowance_pkey");

            entity.ToTable("allowance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("department_pkey");

            entity.ToTable("department");

            entity.HasIndex(e => e.Name, "department_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

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

        modelBuilder.Entity<Period>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("periods_pkey");

            entity.ToTable("periods");

            entity.HasIndex(e => e.Name, "periods_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");

            entity.HasOne(d => d.Department).WithMany(p => p.Roles)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("roles_department_id_fkey");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supervisor_pkey");

            entity.ToTable("supervisor");

            entity.HasIndex(e => e.Name, "supervisor_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
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
            entity.Property(e => e.Version)
                .HasMaxLength(100)
                .HasColumnName("version");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Worker).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("task_worker_id_fkey");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("timetable_pkey");

            entity.ToTable("timetable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChiefAccountant)
                .HasMaxLength(150)
                .HasColumnName("chief_accountant");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_date");
            entity.Property(e => e.DocumentNumber).HasColumnName("document_number");
            entity.Property(e => e.PeriodId).HasColumnName("period_id");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");

            entity.HasOne(d => d.Period).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.PeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_period_id_fkey");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_supervisor_id_fkey");
        });

        modelBuilder.Entity<TimetableRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("timetable_role_pkey");

            entity.ToTable("timetable_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.TimetableId).HasColumnName("timetable_id");

            entity.HasOne(d => d.Role).WithMany(p => p.TimetableRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_role_role_id_fkey");

            entity.HasOne(d => d.Timetable).WithMany(p => p.TimetableRoles)
                .HasForeignKey(d => d.TimetableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_role_timetable_id_fkey");
        });

        modelBuilder.Entity<TimetableRoleAllowance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("timetable_role_allowance_pkey");

            entity.ToTable("timetable_role_allowance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllowanceId).HasColumnName("allowance_id");
            entity.Property(e => e.TimetableRoleId).HasColumnName("timetable_role_id");

            entity.HasOne(d => d.Allowance).WithMany(p => p.TimetableRoleAllowances)
                .HasForeignKey(d => d.AllowanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_role_allowance_allowance_id_fkey");

            entity.HasOne(d => d.TimetableRole).WithMany(p => p.TimetableRoleAllowances)
                .HasForeignKey(d => d.TimetableRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timetable_role_allowance_timetable_role_id_fkey");
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
