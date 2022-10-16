using System;
using System.Collections.Generic;
using System.Text;

namespace Louvor.IPI.Domain.DTO
{
    public static class AnaliseCombinatoria
    {

        public static string combinacao = "";

        public static void GeraNumeroConfirmacao()
        {
            Random random = new Random();

            string numeroConfirmacao = "";

            for (int cont = 0; cont < 2; cont++)
            {
                numeroConfirmacao += random.Next(1, 100).ToString();
            }

            combinacao = numeroConfirmacao;
        }



    }
}
