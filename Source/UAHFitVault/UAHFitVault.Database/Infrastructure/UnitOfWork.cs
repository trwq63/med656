namespace UAHFitVault.Database.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private FitVaultContext dbContext;

        public UnitOfWork(IDbFactory dbFactory) {
            this.dbFactory = dbFactory;
        }

        public FitVaultContext DbContext {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit() {
            DbContext.Commit();
        }
    }
}
