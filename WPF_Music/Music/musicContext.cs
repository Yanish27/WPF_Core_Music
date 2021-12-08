using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WPF_Music.Music
{
    public partial class musicContext : DbContext
    {
        public musicContext()
        {
        }

        public musicContext(DbContextOptions<musicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // Directive #warning
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=music", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
#pragma warning restore CS1030 // Directive #warning
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.IdAlbums)
                    .HasName("PRIMARY");

                entity.ToTable("albums");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.ArtistIdArtist, "fk_Albums_Artist1_idx");

                entity.Property(e => e.IdAlbums).HasColumnName("idAlbums");

                entity.Property(e => e.ArtistIdArtist).HasColumnName("Artist_idArtist");

                entity.Property(e => e.Titre).HasMaxLength(45);

                entity.HasOne(d => d.ArtistIdArtistNavigation)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistIdArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Albums_Artist1");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.IdArtist)
                    .HasName("PRIMARY");

                entity.ToTable("artist");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.IdArtist).HasColumnName("idArtist");

                entity.Property(e => e.Annee).HasMaxLength(4);

                entity.Property(e => e.Discription).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRatings)
                    .HasName("PRIMARY");

                entity.ToTable("ratings");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.AlbumsIdAlbums, "fk_Ratings_Albums_idx");

                entity.Property(e => e.IdRatings).HasColumnName("idRatings");

                entity.Property(e => e.AlbumsIdAlbums).HasColumnName("Albums_idAlbums");

                entity.Property(e => e.Grade).HasMaxLength(45);

                entity.HasOne(d => d.AlbumsIdAlbumsNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.AlbumsIdAlbums)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ratings_Albums");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
