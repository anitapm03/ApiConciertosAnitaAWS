using ApiConciertosAnitaAWS.Data;
using ApiConciertosAnitaAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosAnitaAWS.Repositories
{
    public class RepositoryEventos
    {
        private EventosContext context;

        public RepositoryEventos(EventosContext context)
        {
            this.context = context;
        }

        //mostrar las categorias
        public async Task<List<CategoriaEvento>> GetCategorias()
        {
            return await this.context.Categorias.ToListAsync();
        }

        //mostrar los eventos
        public async Task<List<Evento>> GetEventos()
        {
            return await this.context.Eventos.ToListAsync();
        }

        //mostrar eventos por categoria
        public async Task<List<Evento>> FindEventoCategoria(int idcategoria)
        {
            return await this.context.Eventos
                .Where(x => x.IdCategoria == idcategoria).ToListAsync();
        }
    }
}
