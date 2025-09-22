using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentService.Application.DTOs;
using ContentService.Domain.Entities;

namespace ContentService.Application.Interfaces;

public interface IContentService
{
    Task<IEnumerable<Content>> GetAllContentsAsync();
    Task<Content?> GetContentByIdAsync(Guid id);
    Task<Content> CreateContentAsync(Content content);
    Task<bool> UpdateContentAsync(Guid id, Content content);
    Task<bool> DeleteContentAsync(Guid id);
    Task<bool> UpdateAuthorInfoAsync(Guid authorId, UpdateUserDto newAuthor);

}
