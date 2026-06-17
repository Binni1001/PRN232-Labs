using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class CosmeticsDbContext : DbContext
{
    public CosmeticsDbContext()
    {
    }

    public CosmeticsDbContext(DbContextOptions<CosmeticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CosmeticCategory> CosmeticCategories { get; set; }

    public virtual DbSet<CosmeticInformation> CosmeticInformations { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1TUBGEU\\SQLEXPRESS;Database=CosmeticsDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CosmeticCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Cosmetic__19093A2BF648D5AE");

            entity.ToTable("CosmeticCategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(120);
            entity.Property(e => e.FormulationType).HasMaxLength(250);
            entity.Property(e => e.UsagePurpose).HasMaxLength(250);
        });

        modelBuilder.Entity<CosmeticInformation>(entity =>
        {
            entity.HasKey(e => e.CosmeticId).HasName("PK__Cosmetic__98ED527E2525313C");

            entity.ToTable("CosmeticInformation");

            entity.Property(e => e.CosmeticId)
                .HasMaxLength(30)
                .HasColumnName("CosmeticID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(30)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CosmeticName).HasMaxLength(160);
            entity.Property(e => e.CosmeticSize).HasMaxLength(400);
            entity.Property(e => e.DollarPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ExpirationDate).HasMaxLength(160);
            entity.Property(e => e.SkinType).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.CosmeticInformations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__CosmeticI__Categ__398D8EEE");
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__SystemAc__349DA586DF368844");

            entity.ToTable("SystemAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__SystemAc__49A1474051DD8488").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountNote).HasMaxLength(240);
            entity.Property(e => e.AccountPassword).HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
