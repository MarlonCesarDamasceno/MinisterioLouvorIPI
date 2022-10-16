using System;
using System.Collections.Generic;

#nullable disable

namespace Louvor.IPI.Domain.Entity
{
    public partial class Musica
    {
        public Musica()
        {
            Votacaos = new HashSet<Votacao>();
        }

        public int MusicaId { get; set; }
        public string NomeMusica { get; set; }
        public string ArtistaBanda { get; set; }
        public string LinkYoutube { get; set; }
        public DateTime DataEnvio { get; set; }
        public int UsuarioCriador { get; set; }

        public virtual Usuario UsuarioCriadorNavigation { get; set; }
        public virtual ICollection<Votacao> Votacaos { get; set; }
    }
}
