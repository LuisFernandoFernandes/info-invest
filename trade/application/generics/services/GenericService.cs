using trade.application.entity_framework.services;
using trade.application.generics.models;

namespace trade.application.generics.services
{
    public class GenericService<TModelo> : EntityFrameworkService<TModelo>, IGenericService<TModelo> where TModelo : GenericModel
    {
        public void Dispose() { }
    }

}