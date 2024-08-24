using LeilaoCarro.Models;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Data
{
    public class LeilaoContext(DbContextOptions<LeilaoContext> options) : DbContext(options)
    {
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Lance> Lance { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioEndereco> UsuarioEndereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lance>()
                .HasOne(x => x.Carro)
                .WithMany(x => x.Lances)
                .HasForeignKey(x => x.IdCarro);

            modelBuilder.Entity<Lance>()
                .HasOne(x => x.Usuario)
                .WithMany(x => x.Lances)
                .HasForeignKey(x => x.IdUsuario);

            modelBuilder.Entity<UsuarioEndereco>()
                .HasOne(x => x.Estado)
                .WithMany(x => x.UsuarioEndereco)
                .HasForeignKey(x => x.IdEstado);

            modelBuilder.Entity<UsuarioEndereco>()
                .HasOne(x => x.Usuario)
                .WithMany(x => x.UsuarioEnderecos)
                .HasForeignKey(x => x.IdUsuario);

            base.OnModelCreating(modelBuilder);
        }
    }
}
