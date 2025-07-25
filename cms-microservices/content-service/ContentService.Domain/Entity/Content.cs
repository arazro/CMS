using System;
using ContentService.Domain.Enums;
using ContentService.Domain.ValueObjects;

namespace ContentService.Domain.Entities;

public class Content
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Body { get; set; }
    public string Category { get; set; }
    public Guid AuthorId { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public ContentStatus Status { get; set; } = ContentStatus.Draft;

    public Content()
    {
       
    }
    public Content(string title, string body, string author)
    {
        Title = title;
        Body = body;
        Author = author;
    }

    public void Update(string title, string body, string category)
    {
        Title = title;
        Body = body;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        Status = ContentStatus.Published;
        UpdatedAt = DateTime.UtcNow;
    }
}
