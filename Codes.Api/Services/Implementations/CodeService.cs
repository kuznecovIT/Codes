using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codes.Domain.Entities;
using Codes.Domain.Interfaces.Repositories;
using CodesApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CodesApi.Services.Implementations
{
    /// <inheritdoc />
    class CodeService : ICodeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Code> GetCodeById(Guid codeId) =>
            await _unitOfWork.Codes.GetAsync(codeId);

        /// <inheritdoc />
        public async Task<IEnumerable<Code>> GetAllCodes() =>
            await _unitOfWork.Codes.GetAllAsync();

        /// <inheritdoc />
        public async Task<int> CreateCodes(List<Code> codes)
        {
            if (await CheckDatabaseContainsData())
                await CleanDatabase();

            if (codes.Count > 1)
            {
                var sortedCodes = codes.OrderBy(x => x.CodeType);
                _unitOfWork.Codes.AddRange(sortedCodes);
            }
            else
            {
                _unitOfWork.Codes.Add(codes.First());
            }

            try
            {
                return await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"Error while creating codes in database: {e}");
            }
        }


        private async Task<bool> CheckDatabaseContainsData() =>
            await _unitOfWork.Codes.HasDataAsync();

        private async Task CleanDatabase()
        {
            // Use this only with relation database provider (In Memory database not supported SQL)
            // await _unitOfWork.Codes.DeleteAllData();

            // Less efficient version of the method will be used here
            var data = (await _unitOfWork.Codes.GetAllAsync()).ToList();
            _unitOfWork.Codes.RemoveRange(data);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"Error while deleting codes in database: {e}");
            }
        }
    }
}