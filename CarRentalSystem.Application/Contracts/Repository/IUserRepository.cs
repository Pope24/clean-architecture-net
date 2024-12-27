using CarRentalSystem.Application.Request;
using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity> GetAsyncById(Guid id);
        Task<UserEntity> GetAsyncByUsername(string username);
        Task<UserEntity> GetAsyncByEmail(string email);
        Task<UserEntity> GetAsyncByMobile(string phone);
        Task<bool> SaveAsync(UserEntity user);
        Task<bool> UpdateAsync(UserEntity user);
    }
}
