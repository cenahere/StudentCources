namespace BusinessLayer.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepostiory<T> Entity { get; }
        void Save();
    }
}
