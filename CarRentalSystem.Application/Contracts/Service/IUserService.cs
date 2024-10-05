using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Request;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> GetByEmailAsync(string email);
        Task<UserEntity> GetByUsernameAsync(string username);
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<BaseResponse<UserEntity>> Register(RegisterRequest registerRequest);
        Task<BaseResponse<LoginResponse>> Login(LoginRequest loginRequest);
        Task<BaseResponse<bool>> Active(Guid id);
        string GenerateJwtToken(UserEntity user);
        Task<BaseResponse<UserEntity>> UpdateAsync(UserRequest registerRequest);
        UserEntity RequestConvertToEntity(RegisterRequest registerRequest);
    }
}
