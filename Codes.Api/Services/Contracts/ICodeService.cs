using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codes.Domain.Entities;

namespace Codes.Api.Services.Contracts
{
    /// <summary>
    /// Сервис для работы с кодами
    /// </summary>
    public interface ICodeService
    {
        /// <summary>
        /// Get code by Id
        /// </summary>
        /// <param name="codeId">Searching code id</param>
        /// <returns>Code element or Null if not found</returns>
        public Task<Code> GetCodeById(Guid codeId);

        /// <summary>
        /// Get all codes 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Code>> GetAllCodes();

        /// <summary>
        /// Create codes (sorting by codeType is used before creating) 
        /// </summary>
        /// <param name="codes">Codes to create</param>
        /// <returns>Amount of created elements</returns>
        public Task<int> CreateCodes(List<Code> codes);
    }
}