using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Louvor.IPI.Domain.DTO
{
    public class PropriedadesUsuarios
    {

        public string GetMail(ClaimsPrincipal principal)
        {
            return principal.FindFirst(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}
