using Louvor.IPI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.Interface.Repository
{
    public interface IMusicaRepository
    {
        Task<List<Musica>> ListasMusicas();
    }
}
