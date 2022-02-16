using System.Threading.Tasks;
using Codes.Domain.Entities;

namespace Codes.Domain.Interfaces.Repositories
{
    public interface ICodeRepository : IRepository<Code>
    {
        /// <summary>
        /// Delete all data from Table using 'TRUNCATE TABLE'
        /// </summary>
        /// <returns>Amount of deleted elements</returns>
        public Task<int> DeleteAllData();
    }
}