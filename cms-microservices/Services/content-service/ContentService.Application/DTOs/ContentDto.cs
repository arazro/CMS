using System;

namespace ContentService.Application.DTOs;

public record ContentDto(
    Guid Id,
    string Title,
    string Body,
    string Author,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
