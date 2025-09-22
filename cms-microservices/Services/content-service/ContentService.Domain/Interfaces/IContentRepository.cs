using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentService.Domain.Entities;

namespace ContentService.Domain.Interfaces;

public interface IContentRepository
{
    Task<IEnumerable<Content>> GetAllAsync();
    Task<Content?> GetByIdAsync(Guid id);
    Task AddAsync(Content content);
    Task UpdateAsync(Content content);
    Task DeleteAsync(Guid id);
}
