using Louvor.IPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.Interface.Context
{
    public interface ILouvorContext
    {
        DbSet<Enquete> Enquetes { get; set; }
        DbSet<Musica> Musicas { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Votacao> Votacaos { get; set; }
    }
}
