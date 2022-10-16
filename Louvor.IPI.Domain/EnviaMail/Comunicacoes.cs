using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.EnviaMail
{
   public class Comunicacoes
    {

        public async Task<string> ComunicarCodigoValidacao(string nomeDestinatario, string codigoConfirmacao)
        {
            string comunicacao = string.Join(
                "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> ",
                

                "<html xmlns = \"http://www.w3.org/1999/xhtml\" >",

"<head >",
"<meta http - equiv = \"Content-Type\" content = \"text/html; charset=UTF-8\" />",

"<title >Código de verificação para criação de conta no sistema do IPI</title>", 

"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />",
"</head>",

"<body style=\"margin: 0; padding: 0; \">",
"<center><h1 font=\"arial\">Código de verificação</h1></center>",
$"<p font=\"arial\" size=\"12px\">Olá {nomeDestinatario} tudo bem? Recebemos sua solicitação para criação de um usuario no sistema do Ministério de Louvor da IPI.<br>Aqui está seu código de verificação.<br>{codigoConfirmacao}<br>Volte para tela do sistema e informe o cótido obtido.</p><br>",
"<p font=\"arial\"  size=\"12px\"><strong>***Não responda essa mensagem pois setrata de um envio automático do sistema. Obrigado!</strong><br>www.ministeriolouvoripi.com.br</p>  ",
"</body",
 "</ html >"


                );





            return comunicacao;
        }


        public async Task<string> ComunicarCodigoAlteracaoSenha(string nomeDestinatario, string codigoConfirmacao)
        {
            string comunicacao = string.Join(
                "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> ",


                "<html xmlns = \"http://www.w3.org/1999/xhtml\" >",

"<head >",
"<meta http - equiv = \"Content-Type\" content = \"text/html; charset=UTF-8\" />",

"<title >Código de verificação para alteração de senha no sistema do IPI</title>",

"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" />",
"</head>",

"<body style=\"margin: 0; padding: 0; \">",
"<center><h1 font=\"arial\">Código de verificação</h1></center>",
$"<p font=\"arial\" size=\"12px\">Olá {nomeDestinatario} tudo bem? Iremos te ajudar com a alteração da sua senha.br>Você terá que criar uma nova senha para sua conta. Para isso, informe o código de validação que será direcionado (a) para tela para continuar com sua alteração.<br>Aqui está seu código de verificação.<br>{codigoConfirmacao}<br>Volte para tela do sistema e informe o código obtido.</p><br>",
"<p font=\"arial\"  size=\"12px\"><strong>***Não responda essa mensagem pois setrata de um envio automático do sistema. Obrigado!</strong><br>www.ministeriolouvoripi.com.br</p>  ",
"</body",
 "</ html >"


                );





            return comunicacao;
        }




    }
}
