using Louvor.IPI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.Interface.Repository
{
    public interface IUsuariosRepository
    {
        void AlteraSenha(string senha, int usuarioId);
        void CadastrarUsuario(Usuario usuario);
        Task<Usuario> ValidaUsuarioParaLogin(Usuario usuario);
        
        
        Task<Usuario> VerificaCadastroByMailAsync(Usuario usuario);
        Task<bool> VerificaUsuarioCadastradoNoSistemaAsync(string email);
    }
}
