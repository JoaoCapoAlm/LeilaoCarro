using LeilaoCarro.Models;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Data
{
    public class LeilaoContext(DbContextOptions<LeilaoContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuario { get; set; }
        //public DbSet<UsuarioEndereco> UsuarioEndereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UsuarioEndereco>()
            //    .HasOne(x => x.Usuario)
            //    .WithMany(x => x.UsuarioEnderecos)
            //    .HasForeignKey(x => x.IdUsuario);

            base.OnModelCreating(modelBuilder);
        }
    }
}
