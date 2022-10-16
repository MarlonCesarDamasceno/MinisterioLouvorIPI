using Louvor.IPI.Domain.DTO;
using Louvor.IPI.Domain.EnviaMail;
using Louvor.IPI.Domain.Interface.EnviaMail;
using Louvor.IPI.Domain.Interface.Service;
using Louvor.IPI.Domain.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Louvor.IPI.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService _usuariosService;
        private readonly ICentralComunicacaoMail _centralComunicacaoMail;
        public UsuariosController(IUsuariosService usuariosService, ICentralComunicacaoMail centralComunicacaoMail)
        {
            _usuariosService = usuariosService;
            _centralComunicacaoMail = centralComunicacaoMail;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidacaoCodigoAlteracaoSenha(UsuarioRequestConfirmacaoCodigo usuarioRequestConfirmacaoCodigo)
        {
            
            if(usuarioRequestConfirmacaoCodigo.codigo==AnaliseCombinatoria.combinacao)
            {
                //gera um novo codigo de confirmação para validação interna e envia para outra tela juntamente com o id do usuário.
                AnaliseCombinatoria.GeraNumeroConfirmacao();
                TempData["UsuarioId"] = usuarioRequestConfirmacaoCodigo.UsuarioId;
                TempData["CodigoConfirmacao"] = AnaliseCombinatoria.combinacao;
                
                return RedirectToAction("CadastrarNovaSenha");
            }
            TempData["StatusCodigoConfirmacao"] = true;
            
            return RedirectToAction("RecuperarSenha");
        }

        public async Task<IActionResult> CadastrarNovaSenha()
        {

            if(TempData["UsuarioId"]!=null && TempData["CodigoConfirmacao"]!=null)
            {
                ViewBag.UsuarioId =TempData["UsuarioId"];
                ViewBag.CodigoConfirmacao =TempData["CodigoConfirmacao"];

                
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivacaoAlteracaoSenha(UsuarioRequestConfirmacaoCodigo confirmacao)
        {
            var efetivaAlteracaoSenha = _usuariosService.AlterarSenha(confirmacao.Senha, confirmacao.codigo, confirmacao.UsuarioId);
            if(efetivaAlteracaoSenha)
            {
                TempData["StatusAlteracaoSenha"] = true;
            }
            return RedirectToAction("SenhaAlterada");
        }

        public async Task<IActionResult> SenhaAlterada()
        {
            if(TempData["StatusAlteracaoSenha"]!=null)
            {
                ViewBag.StatusAlteracao = true;
            }
            else
            {
                ViewBag.StatusAlteracao = false;
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarUsuario(UsuarioRequest usuarioRequest)
        {
            var verificaCadastroAtivoByMail = _usuariosService.VerificaCadastroExistenteByMailAsync(usuarioRequest);

            if(verificaCadastroAtivoByMail.Result!=null)
            {
            ViewBag.StatusCadastroAtivo = true;
                ViewBag.UsuarioId = verificaCadastroAtivoByMail.Result.UsuarioId;
                ViewBag.NomeUsuario = verificaCadastroAtivoByMail.Result.NomeUsuario;
                
            }
            else
            {
                ViewBag.StatusCadastroAtivo = false;
            }
            return View();
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
                public async Task<IActionResult> CadastrarNovoUsuario(UsuarioRequest usuarioRequest)
        {
            try
            {

                
                HttpContext.Session.SetString("NomeUsuario", usuarioRequest.Nome);
            HttpContext.Session.SetString("EmailUsuario", usuarioRequest.Email);
            HttpContext.Session.SetString("SenhaUsuario", usuarioRequest.Senha);

            var preCadastro = await _usuariosService.PreCadastroAsync(usuarioRequest.Email, usuarioRequest.Nome);
            if(!preCadastro.StatusContaExiste)
            {
                return RedirectToAction("ObterCodigoUsuario");
            }
            else
            {
                return RedirectToAction("ErroCadastro");
            }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> ErroCadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivarCadastro(UsuarioRequestConfirmacaoCodigo usuarioRequestConfirmacaoCodigo)
        {
            var usuariosRequest = new UsuarioRequest()
            {
                Email = HttpContext.Session.GetString("EmailUsuario"),
                Nome = HttpContext.Session.GetString("NomeUsuario"),
                Senha = HttpContext.Session.GetString("SenhaUsuario")
            };

            var iniciaCadastroUsuario = _usuariosService.CriaUsuario(usuariosRequest, usuarioRequestConfirmacaoCodigo.codigo);
            
            return View(iniciaCadastroUsuario.Result);
        }

        public async Task<IActionResult> GerarCodigo()
        {
            AnaliseCombinatoria.GeraNumeroConfirmacao();
            var comunicacao = new Comunicacoes();
            var propriedadesMail = new PropriedadesMail()
            {
                Assunto = "Reenvio do código de confirmação",
                Destinatario = HttpContext.Session.GetString("EmailUsuario"),
                Mensagem = comunicacao.ComunicarCodigoValidacao(HttpContext.Session.GetString("NomeUsuario"), AnaliseCombinatoria.combinacao).Result
            };

            await _centralComunicacaoMail.EnviarEmail(propriedadesMail);
            return RedirectToAction("ObterCodigoUsuario");
        }


        public async Task<IActionResult> ObterCodigoUsuario()
        {
            return View();
        }

        public async Task<IActionResult> RecuperarSenha()
        {
            if(TempData["StatusCodigoConfirmacao"]!=null)
            {
                ViewBag.StatusCodigoConfirmacao = true;
            }
else
            {
                ViewBag.StatusCodigoConfirmacao = false;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FazerLogin(UsuarioRequest usuarioRequest)
         {
            var usuarioResponse = _usuariosService.AutenticarUsuario(usuarioRequest).Result;

            if(usuarioResponse.NomeUsuario!=null && usuarioResponse.EmailUsuario!=null)
            {
                Claim nomeUsuario = new Claim(ClaimTypes.Name, usuarioResponse.NomeUsuario);
                Claim emailUsuario = new Claim(ClaimTypes.Email, usuarioResponse.EmailUsuario);
                Claim idUsuario = new Claim(ClaimTypes.Hash, "1");


                IList<Claim> claims = new List<Claim>()
                {
                emailUsuario,
                nomeUsuario,
                idUsuario
                };

                ClaimsIdentity identidade = new ClaimsIdentity(claims, "usuarios");


                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidade);


                ClaimsPrincipal cl = new ClaimsPrincipal();
                    HttpContext.SignInAsync(claimsPrincipal);


                HttpContext.User = claimsPrincipal;


                var testeMail =HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email).Value;


                string                 emailUser = claimsPrincipal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email).Value;

                return RedirectToAction("Index", "HomeLogada");
            }
            else
            {
                return RedirectToAction("ErroAutenticacao");
            }
        }

        public async Task<IActionResult> ErroAutenticacao()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }


        
    }
}