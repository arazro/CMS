using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentService.Domain.Entities;
using ContentService.Domain.Interfaces;
using ContentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly ContentDbContext _dbContext;

    public ContentRepository(ContentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Content>> GetAllAsync()
    {
        return await _dbContext.Contents
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Content?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Contents
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Content content)
    {
        await _dbContext.Contents.AddAsync(content);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Content content)
    {
        _dbContext.Contents.Update(content);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var content = await _dbContext.Contents.FindAsync(id);
        if (content is null) return;

        _dbContext.Contents.Remove(content);
        await _dbContext.SaveChangesAsync();
    }
}

