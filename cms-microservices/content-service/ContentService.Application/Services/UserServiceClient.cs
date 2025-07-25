using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContentService.Application.DTOs;
using ContentService.Application.Interfaces;

namespace ContentService.Infrastructure.Services;

public class UserServiceClient(HttpClient _httpClient) : IUserServiceClient
{
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"/api/Users/{id}");
        }
        catch (HttpRequestException ex)
        {
            // Optional: log the error
            return null;
        }
    }

    public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
       var response = await _httpClient.PutAsJsonAsync($"/api/Users/{id}", dto);
       return response.IsSuccessStatusCode;
    }
}
