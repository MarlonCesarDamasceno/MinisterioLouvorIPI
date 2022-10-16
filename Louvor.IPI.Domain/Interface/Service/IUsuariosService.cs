using Louvor.IPI.Domain.Request;
using Louvor.IPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.Interface.Service
{
    public interface IUsuariosService
    {
        bool AlterarSenha(string senha, string codigoConfirmacaoValidacaoInterna, int usuarioId);
        Task<AutenticacaoUsuarioResponse> AutenticarUsuario(UsuarioRequest usuarioRequest);
        Task<UsuariosResponse> CriaUsuario(UsuarioRequest usuarioRequest, string CodigoValidado);
        Task<UsuariosResponse> PreCadastroAsync(string email, string nomeUsuario);
                Task<UsuariosResponse> VerificaCadastroExistenteByMailAsync(UsuarioRequest usuarioRequest);
    }
}
