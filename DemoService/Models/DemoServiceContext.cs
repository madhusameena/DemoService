using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoService.Models
{
    public partial class DemoServiceContext : DbContext
    {
        public DemoServiceContext()
        {
        }

        public DemoServiceContext(DbContextOptions<DemoServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Userdetails> Userdetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=demoservice.database.windows.net;Database=DemoService;user id=ShyamMergu;password=sam@1918");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Project__737584F69BB76DFD")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Project__UserId__6B24EA82");
            });

            modelBuilder.Entity<Userdetails>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.EmailId)
                    .HasName("UQ__Userdeta__7ED91ACE924960EF")
                    .IsUnique();

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
