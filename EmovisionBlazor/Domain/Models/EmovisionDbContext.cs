using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmovisionBlazor.Domain.Models
{
    public partial class EmovisionDbContext
        : IdentityDbContext<User>
    {
        public EmovisionDbContext(DbContextOptions<EmovisionDbContext> options)
            : base(options)
            => Database.EnsureCreated();

        public virtual DbSet<Emotion> Emotions { get; set; } = null!;
        public virtual DbSet<Prediction> Predictions { get; set; } = null!;
        public virtual DbSet<PredictionResult> PredictionResults { get; set; } = null!;
        public virtual DbSet<RecognitionProcess> RecognitionProcesses { get; set; } = null!;
        public virtual DbSet<RecognitionResult> RecognitionResults { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ev_db;Username=ev_root;Password=ev_root;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emotion>(entity =>
            {
                entity.ToTable("emotion");

                entity.HasComment("Таблица эмоции.");

                entity.HasIndex(e => e.Name, "emotion_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Prediction>(entity =>
            {
                entity.ToTable("prediction");

                entity.HasComment("Таблица прогноза");

                entity.HasIndex(e => e.Value, "prediction_value_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<PredictionResult>(entity =>
            {
                entity.HasKey(e => e.RecognitionProcessId)
                    .HasName("prediction_result_pkey");

                entity.ToTable("prediction_result");

                entity.HasComment("Таблица результата распознавания.");

                entity.Property(e => e.RecognitionProcessId)
                    .ValueGeneratedNever()
                    .HasColumnName("recognition_process_id");

                entity.Property(e => e.PredictionId).HasColumnName("prediction_id");

                entity.HasOne(d => d.Prediction)
                    .WithMany(p => p.PredictionResults)
                    .HasForeignKey(d => d.PredictionId)
                    .HasConstraintName("fk_prediction_id");

                entity.HasOne(d => d.RecognitionProcess)
                    .WithOne(p => p.PredictionResult)
                    .HasForeignKey<PredictionResult>(d => d.RecognitionProcessId)
                    .HasConstraintName("fk_recognition_process_id");
            });

            modelBuilder.Entity<RecognitionProcess>(entity =>
            {
                entity.ToTable("recognition_process");

                entity.HasComment("Таблица процесса распознавания");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.RecognitionProcesses)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("fk_session_id");
            });

            modelBuilder.Entity<RecognitionResult>(entity =>
            {
                entity.ToTable("recognition_result");

                entity.HasComment("Таблица результата распознавания.");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmotionId).HasColumnName("emotion_id");

                entity.Property(e => e.RecognitionProcessId).HasColumnName("recognition_process_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.Emotion)
                    .WithMany(p => p.RecognitionResults)
                    .HasForeignKey(d => d.EmotionId)
                    .HasConstraintName("fk_emotion_id");

                entity.HasOne(d => d.RecognitionProcess)
                    .WithMany(p => p.RecognitionResults)
                    .HasForeignKey(d => d.RecognitionProcessId)
                    .HasConstraintName("fk_recognition_process_id");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.HasComment("Таблица сеанса работы пользователя.");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Уникальный номер сеанса.");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("fk_user_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("user_pkey");

                entity.ToTable("user");

                entity.HasComment("Таблица пользователя системы.");

                entity.HasIndex(e => e.Email, "user_email_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.PasswordSha256)
                    .HasMaxLength(64)
                    .HasColumnName("password_sha256")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
