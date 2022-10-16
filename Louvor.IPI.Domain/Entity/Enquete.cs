using System;
using System.Collections.Generic;

#nullable disable

namespace Louvor.IPI.Domain.Entity
{
    public partial class Enquete
    {
        public Enquete()
        {
            Votacaos = new HashSet<Votacao>();
        }

        public int EnqueteId { get; set; }
        public string NomeEnquete { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int UsuarioCriador { get; set; }
        public int FaseEnquete { get; set; }

        public virtual Usuario UsuarioCriadorNavigation { get; set; }
        public virtual ICollection<Votacao> Votacaos { get; set; }
    }
}
