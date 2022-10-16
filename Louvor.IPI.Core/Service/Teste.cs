using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Louvor.IPI.Core.Service
{
 public static  class Teste
    {
        private static ClaimsIdentity GetClaimsIdentity(string usuario="oi")
        {
            return new ClaimsIdentity(new GenericIdentity(usuario));
        }
    }
}
