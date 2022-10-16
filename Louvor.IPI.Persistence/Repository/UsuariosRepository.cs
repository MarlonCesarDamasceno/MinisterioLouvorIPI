using Louvor.IPI.Domain.Entity;
using Louvor.IPI.Domain.Interface.Context;
using Louvor.IPI.Domain.Interface.Repository;
using Louvor.IPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Persistence.Repository
{
    public class UsuariosRepository: IUsuariosRepository
    {
        private readonly LouvorContext _louvorContext;

        public UsuariosRepository(LouvorContext louvorContext)
        {
            _louvorContext = louvorContext;
        }

        public async  Task<bool> VerificaUsuarioCadastradoNoSistemaAsync(string email)
        {
            var usuarioExiste = _louvorContext.Usuarios.Where(usuario => usuario.Email == email);
            
            if (usuarioExiste!=null && usuarioExiste.Count() > 0)
            {

                return true;
            }
            return false;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            _louvorContext.Usuarios.Add(usuario);
            _louvorContext.SaveChanges();
        }


        public async Task<Usuario> VerificaCadastroByMailAsync(Usuario usuario)
        {
            var verificaCadastroMail = _louvorContext.Usuarios.Where(x => x.Email == usuario.Email).FirstOrDefault();

            if(verificaCadastroMail!=null)
            {
                return new Usuario()
                {
                    Nome = verificaCadastroMail.Nome,
                    UsuarioId=verificaCadastroMail.UsuarioId
                };
            }

            return null;
        }

        public void AlteraSenha(string senha, int usuarioId)
        {
            var alteraSenhaUsuario = _louvorContext.Usuarios.Where(x => x.UsuarioId == usuarioId).FirstOrDefault();
            alteraSenhaUsuario.Senha = senha;
            _louvorContext.SaveChanges();
        }
        public async Task<Usuario> ValidaUsuarioParaLogin(Usuario usuario)
        {
            var buscaUsuario = _louvorContext.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == usuario.Senha).FirstOrDefault();

            var usuarioResponse = new Usuario();

            if(buscaUsuario!=null)
            {
                usuarioResponse.Nome = buscaUsuario.Nome;
                usuarioResponse.Email = buscaUsuario.Email;
            
            
            }
            return usuarioResponse;
        }
    }
}
