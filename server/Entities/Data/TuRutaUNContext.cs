using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TuRutaUNAPI.Entities.Data
{
    public partial class TuRutaUNContext : DbContext
    {
        public TuRutaUNContext()
        {
        }

        public TuRutaUNContext(DbContextOptions<TuRutaUNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bu> buses { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<Path> Paths { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteAssigment> RouteAssigments { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DataConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bu>(entity =>
            {
                entity.HasKey(e => e.BusPlate)
                    .HasName("bus_pk");

                entity.ToTable("bus");

                entity.HasIndex(e => e.Number, "bus_number_uk")
                    .IsUnique();

                entity.Property(e => e.BusPlate)
                    .HasMaxLength(10)
                    .HasColumnName("bus_plate");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("color");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("line");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("model");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("driver");

                entity.HasIndex(e => e.Email, "driver_email_uk")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile, "driver_mobile_uk")
                    .IsUnique();

                entity.Property(e => e.DriverId)
                    .HasMaxLength(10)
                    .HasColumnName("driver_id");

                entity.Property(e => e.DriverLicense)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("driver_license");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.FirstSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_surname");

                entity.Property(e => e.IdCard)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("id_card");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("mobile");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("image")
                    .HasColumnName("profile_picture");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(50)
                    .HasColumnName("second_name");

                entity.Property(e => e.SecondSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("second_surname");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("map");

                entity.Property(e => e.MapId).HasColumnName("map_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PathId).HasColumnName("path_id");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.StageId).HasColumnName("stage_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Path)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.PathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("map_fk-path");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("map_fk-route");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("map_fk-stage");
            });

            modelBuilder.Entity<Path>(entity =>
            {
                entity.ToTable("path");

                entity.HasIndex(e => e.PathName, "path_name_uk")
                    .IsUnique();

                entity.Property(e => e.PathId).HasColumnName("path_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PathGpxFile)
                    .IsRequired()
                    .HasColumnType("xml")
                    .HasColumnName("path_gpx_file");

                entity.Property(e => e.PathName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("path_name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.ArriveTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("arrive_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartureTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("departure_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("route_name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RouteAssigment>(entity =>
            {
                entity.ToTable("route_assigment");

                entity.Property(e => e.RouteAssigmentId).HasColumnName("route_assigment_id");

                entity.Property(e => e.BusPlate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("bus_plate");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MapId).HasColumnName("map_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.BusPlateNavigation)
                    .WithMany(p => p.RouteAssigments)
                    .HasForeignKey(d => d.BusPlate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("route_assigment_fk-bus");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.RouteAssigments)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("route_assigment_fk-map");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.ToTable("stage");

                entity.HasIndex(e => e.StageName, "stage_name_uk")
                    .IsUnique();

                entity.Property(e => e.StageId).HasColumnName("stage_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("stage_name");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.XCoordinate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("x_coordinate");

                entity.Property(e => e.YCoordinate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("y_coordinate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "user_email_uk")
                    .IsUnique();

                entity.HasIndex(e => e.Mobile, "user_mobile_uk")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.FirstSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_surname");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("mobile");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("image")
                    .HasColumnName("profile_picture");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(50)
                    .HasColumnName("second_name");

                entity.Property(e => e.SecondSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("second_surname");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_fk-user_type");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_type");

                entity.HasIndex(e => e.Description, "user_type_description_uk")
                    .IsUnique();

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("description");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
