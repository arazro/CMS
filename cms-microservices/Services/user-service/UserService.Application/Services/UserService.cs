using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;
using UserService.Application.Mappers;
using UserService.Application.DTOs;
using System.Linq;
using UserService.Application.Interfaces;
using SharedKernel.Messaging.Events;
using SharedKernel.Messaging.RabbitMQ;

namespace UserService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RabbitMqPublisher _publisher;

        public UserService(IUserRepository userRepository, RabbitMqPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {

            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<UserDto> CreateUserAsync(User user)
        {

            await _userRepository.AddAsync(user);
            return user.ToDto();
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserDto updatedUser)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return false;

           
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;


            await _userRepository.UpdateAsync(existingUser);

            var evt = new UserUpdatedEvent(existingUser.Id, existingUser.Email, existingUser.Name);
            _publisher.Publish("user.exchange", "user.updated", evt);

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        
    }
}
