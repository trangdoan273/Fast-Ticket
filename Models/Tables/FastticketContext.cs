using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace TICKETBOX.Models.Tables;

public partial class FastticketContext : DbContext
{
    public FastticketContext()
    {
    }

    public FastticketContext(DbContextOptions<FastticketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Concession> Concessions { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Showdate> Showdates { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=fastticket;user=root;password=tranmaitrang;allow user variables=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.CinemaId).HasName("PRIMARY");

            entity.ToTable("cinema");

            entity.Property(e => e.CinemaId).HasColumnName("cinemaID");
            entity.Property(e => e.CinemaAddress)
                .HasMaxLength(255)
                .HasColumnName("cinemaAddress");
            entity.Property(e => e.CinemaEmail)
                .HasMaxLength(100)
                .HasColumnName("cinemaEmail");
            entity.Property(e => e.CinemaImage)
                .HasColumnType("text")
                .HasColumnName("cinemaImage");
            entity.Property(e => e.CinemaName)
                .HasMaxLength(50)
                .HasColumnName("cinemaName");
            entity.Property(e => e.CinemaPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("cinemaPhoneNumber");
            entity.Property(e => e.RoomCount).HasColumnName("roomCount");
            entity.Property(e => e.ScreeningType)
                .HasMaxLength(10)
                .HasColumnName("screeningType");
            entity.Property(e => e.SeatCount).HasColumnName("seatCount");
        });

        modelBuilder.Entity<Concession>(entity =>
        {
            entity.HasKey(e => e.ConcessionId).HasName("PRIMARY");

            entity.ToTable("concession");

            entity.Property(e => e.ConcessionId).HasColumnName("concessionID");
            entity.Property(e => e.ConcessionDescription)
                .HasColumnType("text")
                .HasColumnName("concessionDescription");
            entity.Property(e => e.ConcessionImage)
                .HasColumnType("text")
                .HasColumnName("concessionImage");
            entity.Property(e => e.ConcessionName)
                .HasMaxLength(100)
                .HasColumnName("concessionName");
            entity.Property(e => e.ConcessionPrice).HasColumnName("concessionPrice");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Info>(entity =>
        {
            entity.HasKey(e => e.InfoId).HasName("PRIMARY");

            entity.ToTable("info");

            entity.Property(e => e.InfoId).HasColumnName("infoID");
            entity.Property(e => e.InfoContent)
                .HasColumnType("text")
                .HasColumnName("infoContent");
            entity.Property(e => e.InfoImage)
                .HasColumnType("text")
                .HasColumnName("infoImage");
            entity.Property(e => e.InfoTitle)
                .HasColumnType("text")
                .HasColumnName("infoTitle");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("movie");

            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.Duration)
                .HasMaxLength(100)
                .HasColumnName("duration");
            entity.Property(e => e.Language)
                .HasMaxLength(100)
                .HasColumnName("language");
            entity.Property(e => e.MovieActor)
                .HasMaxLength(255)
                .HasColumnName("movieActor");
            entity.Property(e => e.MovieContent)
                .HasColumnType("text")
                .HasColumnName("movieContent");
            entity.Property(e => e.MovieDirector)
                .HasMaxLength(255)
                .HasColumnName("movieDirector");
            entity.Property(e => e.MovieFormat)
                .HasMaxLength(5)
                .HasColumnName("movieFormat");
            entity.Property(e => e.MovieGenre)
                .HasMaxLength(100)
                .HasColumnName("movieGenre");
            entity.Property(e => e.MovieImage)
                .HasColumnType("text")
                .HasColumnName("movieImage");
            entity.Property(e => e.MovieLabel)
                .HasMaxLength(5)
                .HasColumnName("movieLabel");
            entity.Property(e => e.MovieName)
                .HasMaxLength(255)
                .HasColumnName("movieName");
            entity.Property(e => e.MoviePoster)
                .HasColumnType("text")
                .HasColumnName("moviePoster");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("datetime")
                .HasColumnName("releaseDate");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PRIMARY");

            entity.ToTable("seat");

            entity.HasIndex(e => e.MovieId, "seat_movie_idx");

            entity.Property(e => e.SeatId).HasColumnName("seatID");
            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SeatNumb)
                .HasMaxLength(10)
                .HasColumnName("seatNumb");

            entity.HasOne(d => d.Movie).WithMany(p => p.Seats)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("seat_movie");
        });

        modelBuilder.Entity<Showdate>(entity =>
        {
            entity.HasKey(e => e.ShowdateId).HasName("PRIMARY");

            entity.ToTable("showdate");

            entity.HasIndex(e => e.MovieId, "showdate_movie_idx");

            entity.Property(e => e.ShowdateId).HasColumnName("showdateID");
            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.ShowDate1)
                .HasColumnType("datetime")
                .HasColumnName("showDate");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showdates)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("showdate_movie");
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.ShowtimeId).HasName("PRIMARY");

            entity.ToTable("showtime");

            entity.HasIndex(e => e.MovieId, "showtime_movie_idx");

            entity.Property(e => e.ShowtimeId).HasColumnName("showtimeID");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("endTime");
            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("startTime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("showtime_movie");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.MovieId, "ticket_movie_idx");

            entity.HasIndex(e => e.SeatId, "ticket_seat_idx");

            entity.HasIndex(e => e.ShowdateId, "ticket_showdate_idx");

            entity.HasIndex(e => e.ShowtimeId, "ticket_showtime_idx");

            entity.HasIndex(e => e.UserId, "ticket_user_idx");

            entity.Property(e => e.TicketId).HasColumnName("ticketID");
            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.SeatId).HasColumnName("seatID");
            entity.Property(e => e.ShowdateId).HasColumnName("showdateID");
            entity.Property(e => e.ShowtimeId).HasColumnName("showtimeID");
            entity.Property(e => e.TicketStatus)
                .HasColumnType("enum('Booked','Used')")
                .HasColumnName("ticketStatus");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Movie).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("ticket_movie");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("ticket_seat");

            entity.HasOne(d => d.Showdate).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ShowdateId)
                .HasConstraintName("ticket_showdate");

            entity.HasOne(d => d.Showtime).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ShowtimeId)
                .HasConstraintName("ticket_showtime");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ticket_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.DoB).HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("fullName");
            entity.Property(e => e.Role)
                .HasColumnType("enum('User','Admin')")
                .HasColumnName("role");
            entity.Property(e => e.Sex)
                .HasMaxLength(20)
                .HasColumnName("sex");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(255)
                .HasColumnName("userAddress");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("userPhoneNumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
