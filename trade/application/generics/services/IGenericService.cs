using System;
using trade.application.entity_framework.services;
using trade.application.generics.models;

namespace trade.application.generics.services
{
    public interface IGenericService<TModelo> : IEntityFrameworkService<TModelo>, IDisposable where TModelo : class, IGenericModel { }
}