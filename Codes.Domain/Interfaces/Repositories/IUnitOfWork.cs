using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codes.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICodeRepository Codes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}