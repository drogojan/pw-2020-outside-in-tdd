using System;

namespace OpenChat.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}