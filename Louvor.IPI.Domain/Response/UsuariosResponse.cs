using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.Response
{
   public  class UsuariosResponse
    {
        public bool StatusCadastro { get; set; }
        public bool StatusContaExiste { get; set; }
        public bool StatusConfirmacao { get; set; }
        public string NomeUsuario { get; set; }
        public int UsuarioId { get; set; }
    }

    public class AutenticacaoUsuarioResponse
    {
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
    }
}
