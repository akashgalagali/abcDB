using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using abcApi.Models;

#nullable disable

namespace abcApi.Models
{
    public partial class abcDBContext : DbContext
    {
        public abcDBContext()
        {
        }

        public abcDBContext(DbContextOptions<abcDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblMedicine> TblMedicines { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local);integrated security =true;database=abcDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.ToTable("tblCategory");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<TblMedicine>(entity =>
            {
                entity.ToTable("tblMedicines");

                entity.HasIndex(e => e.CidId);

                entity.Property(e => e.Description).HasMaxLength(20);

                entity.Property(e => e.Image).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Seller).HasMaxLength(20);

                entity.HasOne(d => d.Cid)
                    .WithMany(p => p.TblMedicines)
                    .HasForeignKey(d => d.CidId);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUsers");

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.Location).HasMaxLength(20);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasColumnName("role");
            });
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Med).HasMaxLength(20);

                entity.Property(e => e.Cust);

                entity.Property(e => e.Ordered);

                
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<abcApi.Models.TblOrder> TblOrder { get; set; }

    }
}
