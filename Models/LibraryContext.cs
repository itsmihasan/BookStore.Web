using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Rack> Racks { get; set; }

    public virtual DbSet<Shelf> Shelves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207D57EEFA8");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Shelf).WithMany(p => p.Books)
                .HasForeignKey(d => d.ShelfId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Shelves");
        });

        modelBuilder.Entity<Rack>(entity =>
        {
            entity.HasKey(e => e.RackId).HasName("PK__Racks__0363DAA8A616DD48");

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.ShelfId).HasName("PK__Shelves__DBD04F07F4C6CD7F");

            entity.Property(e => e.Code).HasMaxLength(50);

            entity.HasOne(d => d.Rack).WithMany(p => p.Shelves)
                .HasForeignKey(d => d.RackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shelves_Racks");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
