using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trade.application.generics.models;

namespace trade.application.entity_framework.services
{
    public interface IEntityFrameworkService<TModelo> where TModelo : class, IGenericModel
    {
        Task AlterarAsync(List<TModelo> listObjetos);
        Task AlterarAsync(TModelo objeto);
        IQueryable<TModelo> AsQueryable();
        void Detach();
        Task ExcluirAsync(List<string> ids);
        Task ExcluirAsync(List<TModelo> objetos);
        Task ExcluirAsync(string id);
        Task ExcluirAsync(TModelo objeto);
        Task IncluirAsync(TModelo objeto);
        Task IncluirAsync(TModelo[] objeto);
        void SetContext(TradeContexto contexto);
    }
}