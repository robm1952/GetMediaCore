using Microsoft.EntityFrameworkCore;

namespace GetMediaCore.Models;

public partial class MediaContext : DbContext
{
    public MediaContext()
    {
    }

    public MediaContext(DbContextOptions<MediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<ArtistAlbumSongXref> ArtistAlbumSongXrefs { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongFile> SongFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Boomer;Database=Media;Trusted_Connection=True;Encrypt=False");

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
            entity.Property(e => e.ArtistDisplayName)
                .HasMaxLength(250)
                .HasColumnName("artistDisplayName");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(250)
                .HasColumnName("artistName");
        });

        modelBuilder.Entity<ArtistAlbumSongXref>(entity =>
        {
            entity.HasKey(e => e.RefId).HasName("PK_AlbumSongXref");

            entity.ToTable("ArtistAlbumSongXref");

            entity.HasIndex(e => e.RefId, "NonClusteredIndex-20241128-112845");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.GenreId)
                .ValueGeneratedOnAdd()
                .HasColumnName("genreId");
            entity.Property(e => e.GenreName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("genreName");
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
            entity.Property(e => e.SongFileFullyQualifiedName).HasMaxLength(300);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
