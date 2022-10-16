using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.Response
{
    public class MusicasResponse
    {
        public int MusicaId { get; set; }
        public string NomeMusica { get; set; }
        public string BandaArtista { get; set; }
        public string LinkYoutube { get; set; }
        public string DataInclusao { get; set; }
        public int UsuarioCriador { get; set; }
        public string NomeUsuarioCriador { get; set; }
    }
}
