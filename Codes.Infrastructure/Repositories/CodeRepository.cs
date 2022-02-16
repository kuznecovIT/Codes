using System.Threading.Tasks;
using Codes.Domain.Entities;
using Codes.Domain.Interfaces.Repositories;

namespace Codes.Infrastructure.Repositories
{
    public class CodeRepository : Repository<Code>, ICodeRepository
    {
        public CodeRepository(CodesDbContext context) : base(context) { }
        
        /// <inheritdoc />
        public async Task<int> DeleteAllData() => 
            await ExecuteSqlRawAsync($"TRUNCATE TABLE [${nameof(CodesDbContext.Codes)}]");

        private CodesDbContext CodesDbContext => Context as CodesDbContext;
    }
}