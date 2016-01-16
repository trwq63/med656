using System;

namespace UAHFitVault.Database.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        FitVaultContext Init();
    }
}