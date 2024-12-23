using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCusingEFCoreDBFirst.Models;

public partial class QuynhPhuongContext : DbContext
{
    public QuynhPhuongContext()
    {
    }

    public QuynhPhuongContext(DbContextOptions<QuynhPhuongContext> options)
        : base(options)
    {
    }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsType> NewsTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KAB8HQE\\MSSQLSERVER01; Database=QuynhPhuong; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(1000)
                .HasColumnName("Image_URL");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.IsShow).HasColumnName("Is_Show");
            entity.Property(e => e.Summary).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");

            entity.HasOne(d => d.Type).WithMany(p => p.News)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_News_News_Type");
        });

        modelBuilder.Entity<NewsType>(entity =>
        {
            entity.ToTable("News_Type");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("Date_Created");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.IsShow).HasColumnName("Is_Show");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NameAscii)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.IsShow).HasColumnName("Is_Show");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
