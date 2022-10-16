using Louvor.IPI.Domain.EnviaMail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.Interface.EnviaMail
{
    public interface ICentralComunicacaoMail
    {
        Task EnviarEmail(PropriedadesMail propriedadesMail);
    }
}
