using Louvor.IPI.Domain.Entity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Louvor.IPI.Domain.Entity
{
    public partial class Votacao
    {
        public int VotacaoId { get; set; }
        public int UsuarioCriador { get; set; }
        public int Enquete { get; set; }
        public int Musica { get; set; }
        public int QuantidadeEnvioUsuario { get; set; }
        public int QuantidadeVoto { get; set; }

        public virtual Enquete EnqueteNavigation { get; set; }
        public virtual Musica MusicaNavigation { get; set; }
        public virtual Usuario UsuarioCriadorNavigation { get; set; }
    }
}
