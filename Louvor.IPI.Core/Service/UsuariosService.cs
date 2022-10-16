using Microsoft.AspNetCore.Authentication;
using Louvor.IPI.Domain.DTO;
using Louvor.IPI.Domain.Entity;
using Louvor.IPI.Domain.EnviaMail;
using Louvor.IPI.Domain.Interface.EnviaMail;
using Louvor.IPI.Domain.Interface.Repository;
using Louvor.IPI.Domain.Interface.Service;
using Louvor.IPI.Domain.Request;
using Louvor.IPI.Domain.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Core.Service
{
    public class UsuariosService: IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ICentralComunicacaoMail _centralComunicacaoMail;
        
public UsuariosService(IUsuariosRepository usuariosRepository, ICentralComunicacaoMail centralComunicacao)
        {
            _usuariosRepository = usuariosRepository;
            _centralComunicacaoMail = centralComunicacao;
        }

        public bool AlterarSenha(string senha, string codigoConfirmacaoValidacaoInterna, int usuarioId)
        {
            try
            {

                if(codigoConfirmacaoValidacaoInterna==AnaliseCombinatoria.combinacao)
                {

                    var cliptografaSenha = new Cliptografia();

                    _usuariosRepository.AlteraSenha(cliptografaSenha.CliptografaSenha(senha), usuarioId);

                    return true;
                }

                return false;
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }


        public async Task<UsuariosResponse> PreCadastroAsync(string email, string nomeUsuario)
        {
            try
            {
                var usuariosResponses = new UsuariosResponse();
                var verificaUsuarioExiste = await _usuariosRepository.VerificaUsuarioCadastradoNoSistemaAsync(email);

                if(!verificaUsuarioExiste)
                {
                    usuariosResponses.StatusContaExiste = true;
                }
                else
                {

                    AnaliseCombinatoria.GeraNumeroConfirmacao();

                    var comunicacaoCodigoValidacao = new Comunicacoes();

                    var propriedadesMail = new PropriedadesMail()
                    {
                        Assunto="Solicitação de criação de conta - código de validação",
                        Destinatario=email,
                        Mensagem=comunicacaoCodigoValidacao.ComunicarCodigoValidacao(nomeUsuario, AnaliseCombinatoria.combinacao).Result
                    };
                  await  _centralComunicacaoMail.EnviarEmail(propriedadesMail);
                    
                }

                return usuariosResponses;
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public async Task<UsuariosResponse> CriaUsuario(UsuarioRequest usuarioRequest, string CodigoValidado)
        {
            try
            {
                var usuarioResponse = new UsuariosResponse();
                var cliptografaSh1 = new Cliptografia();

                if(CodigoValidado== AnaliseCombinatoria.combinacao)
                {

                  var usuariosToEntity = new Usuario()
                    {
                        Email=usuarioRequest.Email,
                        Nome=usuarioRequest.Nome,
                        Senha=cliptografaSh1.CliptografaSenha(usuarioRequest.Senha)
                                            };

                    _usuariosRepository.CadastrarUsuario(usuariosToEntity);
                    usuarioResponse.StatusConfirmacao = true;
                    usuarioResponse.StatusCadastro = true;
                }

                AnaliseCombinatoria.combinacao = "";
                return usuarioResponse;
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public async Task<AutenticacaoUsuarioResponse> AutenticarUsuario(UsuarioRequest usuarioRequest)
        {
            try
            {
                var response = new AutenticacaoUsuarioResponse();
                var cliptografaSenha = new Cliptografia();
                var usuarioRequestParaUsuarioEntity = new Usuario()
                {
                    Email=usuarioRequest.Email,
                    Senha=cliptografaSenha.CliptografaSenha(usuarioRequest.Senha)
                };

                var autenticaUsuario = _usuariosRepository.ValidaUsuarioParaLogin(usuarioRequestParaUsuarioEntity).Result;
                
                if(autenticaUsuario.Email!=null && autenticaUsuario.Nome!=null)
                {
                    response.NomeUsuario = autenticaUsuario.Nome;
                    response.EmailUsuario = autenticaUsuario.Email;
                }

                return response;
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public async Task<UsuariosResponse> VerificaCadastroExistenteByMailAsync(UsuarioRequest usuarioRequest)
        {
            try
            {
                var comunicacao = new Comunicacoes();
                var usuarioEntity = new Usuario()
                {
                    Email=usuarioRequest.Email
                };


                var dadosUsuario = _usuariosRepository.VerificaCadastroByMailAsync(usuarioEntity).Result;

                if(dadosUsuario!=null)
                {
                    AnaliseCombinatoria.GeraNumeroConfirmacao();

                    var propriedadesMail = new PropriedadesMail()
                    {
                        Assunto = "Solicitação de alteração de senha - código de verificação",
                        Destinatario =usuarioRequest.Email,
                        Mensagem = comunicacao.ComunicarCodigoAlteracaoSenha(dadosUsuario.Nome, AnaliseCombinatoria.combinacao).Result
                    };
                    await _centralComunicacaoMail.EnviarEmail(propriedadesMail);

                    return new UsuariosResponse()
                    {
                        UsuarioId = dadosUsuario.UsuarioId,
                        NomeUsuario = dadosUsuario.Nome
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
