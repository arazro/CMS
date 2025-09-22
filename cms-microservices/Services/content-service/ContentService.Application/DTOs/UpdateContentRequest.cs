namespace ContentService.Application.DTOs;

public class UpdateContentRequest
{
    public string Title { get; init; } = default!;
    public string Body { get; init; } = default!;
}
