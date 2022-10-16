using Louvor.IPI.Domain.Interface.Repository;
using Louvor.IPI.Domain.Interface.Service;
using Louvor.IPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Louvor.IPI.Core.Service
{
    public  class MusicaService: IMusicaService
    {
        private IMusicaRepository _musicaRepository;

        public MusicaService(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task<List<MusicasResponse>> ListarMusicas()
        {
            try
            {
                
                var musicas = _musicaRepository.ListasMusicas();
                
                if (musicas == null)
                    throw new ArgumentException("Não encontrado músicas cadastradas.");

                List<MusicasResponse> musicasResponses = new List<MusicasResponse>();

                foreach (var musicasCadastradas in musicas.Result)
                {
                    musicasResponses.Add(new MusicasResponse()
                    {
                            BandaArtista = musicasCadastradas.ArtistaBanda,
                       DataInclusao = musicasCadastradas.DataEnvio.ToString("dd/MM/yyyy"),
                        LinkYoutube = musicasCadastradas.LinkYoutube,
                        MusicaId = musicasCadastradas.MusicaId,
                        NomeMusica = musicasCadastradas.NomeMusica,
                        UsuarioCriador = musicasCadastradas.UsuarioCriador,
                        NomeUsuarioCriador=musicasCadastradas.UsuarioCriadorNavigation.Nome
                    });

                        
                }
                return musicasResponses.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}
