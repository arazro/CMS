using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(Guid id, UserDto user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
