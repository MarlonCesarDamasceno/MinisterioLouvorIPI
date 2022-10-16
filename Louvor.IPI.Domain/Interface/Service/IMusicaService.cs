using Louvor.IPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Domain.Interface.Service
{
    public interface IMusicaService
    {
        Task<List<MusicasResponse>> ListarMusicas();
    }
}
