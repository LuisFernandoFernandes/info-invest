namespace trade.application.generics.models
{
    public interface IGenericResult<T>
    {
        T data { get; set; }
        string message { get; set; }
        bool success { get; set; }
    }
}