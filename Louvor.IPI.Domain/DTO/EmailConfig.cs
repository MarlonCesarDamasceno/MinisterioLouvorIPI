using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.DTO
{
    public class EmailConfig
    {
        public string Smtp { get; set; }
        public int Porta { get; set; }
        public string ContaEmail { get; set; }
        public string Senha { get; set; }
        public string FromEmail { get; set; }
    }
}
