using Louvor.IPI.Domain.DTO;
using Louvor.IPI.Domain.Interface.EnviaMail;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.EnviaMail
{
    public class CentralMailComunicacao : ICentralComunicacaoMail
    {
        public CentralMailComunicacao(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }
        public EmailConfig _emailConfig { get; }

        public async Task EnviarEmail(PropriedadesMail propriedadesMail)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailConfig.ContaEmail, "Comunicacao Ministério de Louvor IPI"),
                };

                mailMessage.To.Add(new MailAddress(propriedadesMail.Destinatario));
                mailMessage.Subject = "Comunicação - Ministério de Louvor IPI " + propriedadesMail.Assunto;
                mailMessage.Body = propriedadesMail.Mensagem;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;


                using (SmtpClient smtp = new SmtpClient(_emailConfig.Smtp, _emailConfig.Porta))
                {
                    smtp.Credentials = new NetworkCredential(_emailConfig.ContaEmail, _emailConfig.Senha);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mailMessage);
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }
    }
}
