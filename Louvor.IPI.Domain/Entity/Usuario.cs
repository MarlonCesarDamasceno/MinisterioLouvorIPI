using System;
using System.Collections.Generic;

#nullable disable

namespace Louvor.IPI.Domain.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            Enquetes = new HashSet<Enquete>();
            Musicas = new HashSet<Musica>();
            Votacaos = new HashSet<Votacao>();
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Enquete> Enquetes { get; set; }
        public virtual ICollection<Musica> Musicas { get; set; }
        public virtual ICollection<Votacao> Votacaos { get; set; }
    }
}
