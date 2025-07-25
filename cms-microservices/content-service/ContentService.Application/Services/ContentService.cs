using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentService.Application.DTOs;
using ContentService.Application.Interfaces;
using ContentService.Domain.Entities;
using ContentService.Domain.Enums;
using ContentService.Domain.Interfaces;
using ContentService.Domain.ValueObjects;
using Humanizer;

namespace ContentService.Application.Services;

public class ContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly IUserServiceClient _userClient;

    public ContentService(IContentRepository contentRepository, IUserServiceClient userClient)
    {
        _contentRepository = contentRepository;
        _userClient = userClient;
    }

    public async Task<IEnumerable<Content>> GetAllContentsAsync()
    {
        return await _contentRepository.GetAllAsync();
    }

    public async Task<Content?> GetContentByIdAsync(Guid id)
    {
        return await _contentRepository.GetByIdAsync(id);
    }

    public async Task<Content> CreateContentAsync(Content content)
    {
        var user = await _userClient.GetUserByIdAsync(content.AuthorId);
        if (user == null)
            throw new Exception("Invalid AuthorId: User not found.");

        content.CreatedAt = DateTime.UtcNow;
        content.Status = ContentStatus.Archived;
        content.Author = user.Name;
        await _contentRepository.AddAsync(content);
        return content;
    }

    public async Task<bool> UpdateContentAsync(Guid id, Content updatedContent)
    {
        var existing = await _contentRepository.GetByIdAsync(id);
        if (existing == null) return false;

        existing.Title = updatedContent.Title;
        existing.Body = updatedContent.Body;
        existing.Category = updatedContent.Category;
        existing.UpdatedAt = DateTime.UtcNow;

        await _contentRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteContentAsync(Guid id)
    {
        var existing = await _contentRepository.GetByIdAsync(id);
        if (existing == null) return false;

        await _contentRepository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> UpdateAuthorInfoAsync(Guid authorId, UpdateUserDto newAuthor)
{
    var updated = await _userClient.UpdateUserAsync(authorId, newAuthor);

    if (!updated)
        throw new Exception("Failed to update user in UserService");

    return true;
}
}
