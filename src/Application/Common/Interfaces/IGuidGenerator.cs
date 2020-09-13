using System;
using System.Threading.Tasks;

namespace OpenChat.Application.Common.Interfaces
{
    public interface IGuidGenerator
    {
        Task<Guid> GetNextAsync();
    }
}