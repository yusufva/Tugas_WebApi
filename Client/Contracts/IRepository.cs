using WebApi.Utilities.Handler;

namespace Client.Contracts
{
    public interface IRepository<T, Y, X> where T : class where Y : class
    {
        Task<ResponseOkHandler<IEnumerable<T>>> Get();
        Task<ResponseOkHandler<T>> Get(X id);
        Task<ResponseOkHandler<Y>> Post(Y entity);
        Task<ResponseOkHandler<T>> Put(T entity);
        Task<ResponseOkHandler<T>> Delete(X id);
    }
}
