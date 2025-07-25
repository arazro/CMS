namespace ContentService.Application.DTOs;

public class CreateContentRequest
{
    public string Title { get; init; } = default!;
    public string Body { get; init; } = default!;
    public string Author { get; init; } = default!;
}
