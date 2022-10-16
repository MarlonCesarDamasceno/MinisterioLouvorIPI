using Louvor.IPI.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Louvor.IPI.Controllers
{
    public class MusicasController : Controller
    {
        private readonly IMusicaService _musicaService;

        public MusicasController(IMusicaService musicaService)
        {
            _musicaService = musicaService;
        }

        public IActionResult Index()
        {

            var listaMusicas = _musicaService.ListarMusicas();
            

            return View(listaMusicas.Result);
        }
    }
}
