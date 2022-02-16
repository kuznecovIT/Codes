using System.Threading;
using System.Threading.Tasks;
using Codes.Domain.Interfaces.Repositories;
using Codes.Infrastructure.Repositories;

namespace Codes.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodesDbContext _context;

        public UnitOfWork(CodesDbContext context)
        {
            _context = context;
            Codes = new CodeRepository(_context);
        }

        /// <inheritdoc />
        public ICodeRepository Codes { get; private set; }
        
        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <inheritdoc />
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}