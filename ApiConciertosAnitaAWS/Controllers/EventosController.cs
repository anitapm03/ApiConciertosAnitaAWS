using ApiConciertosAnitaAWS.Models;
using ApiConciertosAnitaAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiConciertosAnitaAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private RepositoryEventos repo;

        public EventosController(RepositoryEventos repo) 
        {
            this.repo = repo;   
        }

        //get categorias
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CategoriaEvento>>>
            GetCategorias()
        {
            return await this.repo.GetCategorias();
        }

        //get eventos
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Evento>>>
            GetEventos()
        {
            return await this.repo.GetEventos();
        }

        //find evento categoria
        [HttpGet]
        [Route("[action]/{idcategoria}")]
        public async Task<ActionResult<List<Evento>>>
            FindEventoCategoria(int idcategoria)
        {
            return await this.repo.FindEventoCategoria(idcategoria);
        }

    }
}
