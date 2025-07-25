using System;

namespace ContentService.Domain.Exceptions;

public class ContentNotFoundException : Exception
{
    public ContentNotFoundException(Guid id)
        : base($"Content with id '{id}' was not found.")
    {
    }
}
