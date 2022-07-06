using System;

namespace trade.application.generics.models
{
    public interface IGenericModel : IDisposable
    {
        string Id { get; set; }
    }
}
