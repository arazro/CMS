using ContentService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application.Interfaces
{
    public interface IUserServiceClient
    {
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserAsync(Guid id, UpdateUserDto dto);
    }
    
}
