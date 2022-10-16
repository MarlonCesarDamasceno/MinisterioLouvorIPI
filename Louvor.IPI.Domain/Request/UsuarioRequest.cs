using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int UsuarioId { get; set; }
    }

    public class UsuarioRequestConfirmacaoCodigo : UsuarioRequest
    {
        public string codigo { get; set; }
            }
}
