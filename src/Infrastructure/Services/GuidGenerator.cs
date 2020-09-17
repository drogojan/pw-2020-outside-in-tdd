using System;
using System.Threading.Tasks;
using OpenChat.Application.Common.Interfaces;

namespace OpenChat.Infrastructure.Services
{
    public class GuidGenerator : IGuidGenerator
    {
        public async Task<Guid> GetNextAsync()
        {
            return await Task.FromResult(Guid.NewGuid());
        }
    }
}