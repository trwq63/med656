namespace UAHFitVault.Database.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        FitVaultContext dbContext;

        public FitVaultContext Init() {
            return dbContext ?? (dbContext = new FitVaultContext());
        }

        public void Dispose() {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}