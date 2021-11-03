using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TuRutaUN.Entities.Login
{
    public partial class ExternalLoginDBContext : DbContext
    {
        public ExternalLoginDBContext()
        {
        }

        public ExternalLoginDBContext(DbContextOptions<ExternalLoginDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginUser> LoginUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:LoginConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LoginUser>(entity =>
            {
                entity.ToTable("login_user");

                entity.HasIndex(e => e.Username, "login_user_username_uk")
                    .IsUnique();

                entity.Property(e => e.LoginUserId)
                    .HasMaxLength(10)
                    .HasColumnName("login_user_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .HasColumnName("password_salt");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
