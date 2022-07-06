using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using trade.application.generics.models;
using trade.application.injector;

namespace trade.application.entity_framework.services
{
    public class EntityFrameworkService<TModelo> : IEntityFrameworkService<TModelo> where TModelo : class, IGenericModel
    {
        private TradeContexto _contexto;

        public void Detach()
        {
            var local = _contexto.Set<TModelo>().Local;
            local.ToList().ForEach(p => _contexto.Entry(p).State = EntityState.Detached);
        }

        public EntityFrameworkService() { _contexto = Factory.InstanceOf<TradeContexto>(); }

        public void SetContext(TradeContexto contexto)
        {
            _contexto = contexto;
        }

        private void Acao(TradeContexto contexto, TModelo objeto, EntityState estado)
        {
            contexto.Set<TModelo>().Attach(objeto);
            contexto.Entry(objeto).State = estado;
        }

        public virtual async Task AlterarAsync(TModelo objeto)
        {
            Acao(_contexto, objeto, EntityState.Modified);
            await _contexto.SaveChangesAsync();
        }

        public virtual async Task AlterarAsync(List<TModelo> listObjetos)
        {
            foreach (var objeto in listObjetos) { await AlterarAsync(objeto); }
        }

        public virtual async Task ExcluirAsync(string id)
        {
            TModelo objContexto = await _contexto.Set<TModelo>().FindAsync(id);
            if (objContexto == null) { return; }
            _contexto.Set<TModelo>().Remove(objContexto);
            await _contexto.SaveChangesAsync();
        }

        public virtual async Task ExcluirAsync(List<string> ids)
        {
            foreach (var id in ids) { await ExcluirAsync(id); }
        }

        public virtual async Task ExcluirAsync(TModelo objeto)
        {
            Acao(_contexto, objeto, EntityState.Deleted);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirAsync(List<TModelo> objetos)
        {
            foreach (var objeto in objetos) { await ExcluirAsync(objeto); }
        }

        public virtual async Task IncluirAsync(TModelo objeto)
        {
            _contexto.Set<TModelo>().Add(objeto);
            await _contexto.SaveChangesAsync();
        }

        public async Task IncluirAsync(TModelo[] objeto)
        {
            _contexto.Set<TModelo>().AddRange(objeto);
            await _contexto.SaveChangesAsync();
        }

        public IQueryable<TModelo> AsQueryable() { return _contexto.Set<TModelo>().AsQueryable<TModelo>(); }
    }
}