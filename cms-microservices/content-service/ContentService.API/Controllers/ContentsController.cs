using Microsoft.AspNetCore.Mvc;
using ContentService.Domain.Entities;
using ContentService.Application.Interfaces;
using System.Threading.Tasks;
using System;
using ContentService.Application.DTOs;

namespace ContentService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentsController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contents = await _contentService.GetAllContentsAsync();
        return Ok(contents);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var content = await _contentService.GetContentByIdAsync(id);
        if (content == null)
            return NotFound();

        return Ok(content);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Content content)
    {
        var createdContent = await _contentService.CreateContentAsync(content);
        return CreatedAtAction(nameof(GetById), new { id = createdContent.Id }, createdContent);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Content content)
    {
        var result = await _contentService.UpdateContentAsync(id, content);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _contentService.DeleteContentAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("authors/{id}")]
    public async Task<IActionResult> UpdateAuthorName(Guid id, [FromBody] UpdateUserDto newAuthor)
    {
        var result = await _contentService.UpdateAuthorInfoAsync(id, newAuthor);
        return result ? Ok() : StatusCode(500);
    }


}