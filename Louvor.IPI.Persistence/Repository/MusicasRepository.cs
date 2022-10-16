using Louvor.IPI.Domain.Entity;
using Louvor.IPI.Domain.Interface.Context;
using Louvor.IPI.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Persistence.Repository
{
   public class MusicasRepository: IMusicaRepository
    {
        private readonly ILouvorContext _louvorContext;

        public MusicasRepository(ILouvorContext louvorContext)
        {
            _louvorContext = louvorContext;
        }

        public async Task<List<Musica>> ListasMusicas()
        {
            var dadosMusicas = _louvorContext.Musicas.Include(x => x.UsuarioCriadorNavigation).ToList();
            
            return dadosMusicas;
        }
    }
}
