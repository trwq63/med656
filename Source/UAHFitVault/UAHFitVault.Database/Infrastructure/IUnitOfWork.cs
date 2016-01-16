namespace UAHFitVault.Database.Infrastructure
{
    /// <summary>
    /// Interface for unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        void Commit();
    }
}
