using System;
using Louvor.IPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Louvor.IPI.Persistence.Models
{
    public partial class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Enquete> Enquetes { get; set; }
        public virtual DbSet<Musica> Musicas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Votacao> Votacaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=louvor;uid=root;pwd=isathi", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Enquete>(entity =>
            {
                entity.ToTable("enquete");

                entity.HasIndex(e => e.UsuarioCriador, "fk_usuario_criador_enquete");

                entity.Property(e => e.EnqueteId).HasColumnName("enqueteId");

                entity.Property(e => e.DataFim)
                    .HasColumnType("datetime")
                    .HasColumnName("dataFim");

                entity.Property(e => e.DataInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("dataInicio");

                entity.Property(e => e.FaseEnquete).HasColumnName("faseEnquete");

                entity.Property(e => e.NomeEnquete)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nomeEnquete");

                entity.Property(e => e.UsuarioCriador).HasColumnName("usuarioCriador");

                entity.HasOne(d => d.UsuarioCriadorNavigation)
                    .WithMany(p => p.Enquetes)
                    .HasForeignKey(d => d.UsuarioCriador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_criador_enquete");
            });

            modelBuilder.Entity<Musica>(entity =>
            {
                entity.ToTable("musicas");

                entity.HasIndex(e => e.UsuarioCriador, "fk_usuario_criador");

                entity.Property(e => e.MusicaId).HasColumnName("musicaId");

                entity.Property(e => e.ArtistaBanda)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("artistaBanda");

                entity.Property(e => e.DataEnvio)
                    .HasColumnType("datetime")
                    .HasColumnName("dataEnvio");

                entity.Property(e => e.LinkYoutube)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("linkYoutube");

                entity.Property(e => e.NomeMusica)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nomeMusica");

                entity.Property(e => e.UsuarioCriador).HasColumnName("usuarioCriador");

                entity.HasOne(d => d.UsuarioCriadorNavigation)
                    .WithMany(p => p.Musicas)
                    .HasForeignKey(d => d.UsuarioCriador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_criador");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(90)
                    .HasColumnName("email");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("senha");
            });

            modelBuilder.Entity<Votacao>(entity =>
            {
                entity.ToTable("votacao");

                entity.HasIndex(e => e.Enquete, "fk_enquete_vigente");

                entity.HasIndex(e => e.Musica, "fk_musica_enviada_votacao");

                entity.HasIndex(e => e.UsuarioCriador, "fk_usuario_criador_votacao");

                entity.Property(e => e.VotacaoId).HasColumnName("votacaoId");

                entity.Property(e => e.Enquete).HasColumnName("enquete");

                entity.Property(e => e.Musica).HasColumnName("musica");

                entity.Property(e => e.QuantidadeEnvioUsuario).HasColumnName("quantidadeEnvioUsuario");

                entity.Property(e => e.QuantidadeVoto).HasColumnName("quantidadeVoto");

                entity.Property(e => e.UsuarioCriador).HasColumnName("usuarioCriador");

                entity.HasOne(d => d.EnqueteNavigation)
                    .WithMany(p => p.Votacaos)
                    .HasForeignKey(d => d.Enquete)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_enquete_vigente");

                entity.HasOne(d => d.MusicaNavigation)
                    .WithMany(p => p.Votacaos)
                    .HasForeignKey(d => d.Musica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_musica_enviada_votacao");

                entity.HasOne(d => d.UsuarioCriadorNavigation)
                    .WithMany(p => p.Votacaos)
                    .HasForeignKey(d => d.UsuarioCriador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_criador_votacao");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
