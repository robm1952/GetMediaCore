using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GetMediaCore.Models;

public partial class MediaCore : DbContext
{
    public MediaCore()
    {
    }

    public MediaCore(DbContextOptions<MediaCore> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<ArtistAlbumSongXref> ArtistAlbumSongXrefs { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<PlaylistHistory> PlaylistHistories { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongFile> SongFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Boomer2;Database=MediaCore;Trusted_Connection=True;Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK_Album");

            entity.Property(e => e.AlbumId).HasColumnName("albumId");
            entity.Property(e => e.AlbumArtistId).HasColumnName("albumArtistId");
            entity.Property(e => e.AlbumDisc).HasColumnName("albumDisc");
            entity.Property(e => e.AlbumGenre).HasColumnName("albumGenre");
            entity.Property(e => e.AlbumTitle)
                .HasMaxLength(250)
                .HasColumnName("albumTitle");
            entity.Property(e => e.AlbumYear).HasColumnName("albumYear");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK_Artist");

            entity.Property(e => e.ArtistId).HasColumnName("artistId");
            entity.Property(e => e.ArtistSortName)
                .HasMaxLength(250)
                .HasColumnName("artistSortName");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(250)
                .HasColumnName("artistName");
        });

        modelBuilder.Entity<ArtistAlbumSongXref>(entity =>
        {
            entity.HasKey(e => e.RefId).HasName("PK_AlbumSongXref");

            entity.ToTable("ArtistAlbumSongXref");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId);
            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genreName");
        });

        modelBuilder.Entity<PlaylistHistory>(entity =>
        {
            entity.HasKey(e => e.M3uid);

            entity.ToTable("PlaylistHistory");

            entity.Property(e => e.M3uid).HasColumnName("M3UID");
            entity.Property(e => e.Album).HasMaxLength(150);
            entity.Property(e => e.Artist).HasMaxLength(175);
            entity.Property(e => e.Genre).HasMaxLength(25);
            entity.Property(e => e.M3uname)
                .HasMaxLength(50)
                .HasColumnName("M3UName");
            entity.Property(e => e.SongTitle).HasMaxLength(250);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.Property(e => e.SongId).HasColumnName("songID");
            entity.Property(e => e.SongAlbumId).HasColumnName("songAlbumId");
            entity.Property(e => e.SongDuration).HasColumnName("songDuration");
            entity.Property(e => e.SongTitle)
                .HasMaxLength(250)
                .HasColumnName("songTitle");
            entity.Property(e => e.SongTrackNumber).HasColumnName("songTrackNumber");
        });

        modelBuilder.Entity<SongFile>(entity =>
        {
            entity.Property(e => e.SongFileFqn)
                .HasMaxLength(300)
                .HasColumnName("SongFileFQN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
