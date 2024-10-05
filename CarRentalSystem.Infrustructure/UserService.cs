using BCrypt.Net;
using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Application.Request;
using CarRentalSystem.Application.Validations;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalSystem.Infrustructure
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailSenderService emailSenderService = new EmailSenderService();

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return userRepository.GetAllAsync();
        }

        public Task<UserEntity> GetByEmailAsync(string email)
        {
            return userRepository.GetAsyncByEmail(email);
        }

        public Task<UserEntity> GetByUsernameAsync(string username)
        {
            return userRepository.GetAsyncByUsername(username);
        }


        public async Task<BaseResponse<UserEntity>> Register(RegisterRequest registerRequest)
        {
            var response = new BaseResponse<UserEntity>();
            await ValidationRegisterRequest(registerRequest, response);
            if (response.Errors.Count > 0)
            {
                response.Succeeded = true;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            var entity = RequestConvertToEntity(registerRequest);
            await userRepository.SaveAsync(entity);
            response.Succeeded = true;
            response.Message = "Đăng ký người dùng mới thành công, chúng tôi đã gửi link kích hoạt tài khoản qua email bạn đã đăng ký, vui lòng kích hoạt để đăng nhập";
            response.StatusCode = System.Net.HttpStatusCode.OK;
            emailSenderService.SendEmail(entity, EContentEmail.AccountActive);
            return response;
        }

        public Task<BaseResponse<UserEntity>> UpdateAsync(UserRequest registerRequest)
        {
            throw new NotImplementedException();
        }
        public UserEntity RequestConvertToEntity(RegisterRequest registerRequest)
        {
            var entity = new UserEntity()
            {
                UserName = registerRequest.Username,
                DisplayName = registerRequest.DisplayName,
                Email = registerRequest.Email,
                Mobile = registerRequest.Mobile,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                Role = ERole.Customer,
                SignInMethod = ESignInMethod.Default,
                VerifyStatus = EVerifyStatus.Unverified,
                Address = registerRequest.Address,
                Status = EUserStatus.Inactive,
                TextSearch = registerRequest.DisplayName + " " + registerRequest.Email + " " + registerRequest.Mobile
            };
            return entity;
        }
        public async Task ValidationRegisterRequest(RegisterRequest registerRequest, BaseResponse<UserEntity> response)
        {
            if (CommonValidationRule.IsNullOrEmpty(registerRequest.Username))
            {
                response.Errors.Add("Username", "Tên đăng nhập không được để trống");
            } 
            else if ((await GetByUsernameAsync(registerRequest.Username)) != null)
            {
                response.Errors.Add("Username", "Tên đăng nhập đã được sử dụng");
            }
            if (CommonValidationRule.IsNullOrEmpty(registerRequest.DisplayName))
            {
                response.Errors.Add("DisplayName", "Tên hiển thị không được để trống");
            }
            if (CommonValidationRule.IsNullOrEmpty(registerRequest.Address))
            {
                response.Errors.Add("Address", "Địa chỉ không được để trống");
            }
            if (!CommonValidationRule.IsValidEmail(registerRequest.Email))
            {
                response.Errors.Add("Email", "Email của bạn không hợp lệ");
            }
            else if ((await GetByEmailAsync(registerRequest.Email)) != null)
            {
                response.Errors.Add("Email", "Email đã được sử dụng");
            }
            if (!CommonValidationRule.IsValidPhoneNumber(registerRequest.Mobile))
            {
                response.Errors.Add("Mobile", "Số điện thoại không hợp lệ");
            }
            else if ((await GetByEmailAsync(registerRequest.Email)) != null)
            {
                response.Errors.Add("Mobile", "Số điện thoại đã được sử dụng");
            }
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await userRepository.GetAsyncById(id);
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var response = new BaseResponse<LoginResponse>();
            await ValidationLoginRequest(loginRequest, response);
            if (response.Errors.Count > 0)
            {
                response.Succeeded = true;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            } 
            else
            {
                var user = await userRepository.GetAsyncByUsername(loginRequest.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
                {
                    return new BaseResponse<LoginResponse>()
                    {
                        Succeeded = true,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Errors = new Dictionary<string, string>()
                        {
                            {"LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng" }
                        }
                    };
                }
                else if (user.Status == EUserStatus.Inactive)
                {
                    return new BaseResponse<LoginResponse>()
                    {
                        Succeeded = true,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Errors = new Dictionary<string, string>()
                        {
                            { "AccountInactive", "Tài khoản chưa được kích hoạt, vui lòng kích hoạt qua link đã được gửi qua email" }
                        }
                    };
                }
                else
                {
                    var token = GenerateJwtToken(user);
                    return new BaseResponse<LoginResponse>()
                    {
                        Succeeded = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Errors = new Dictionary<string, string>()
                        {
                            {"LoginSucess", "Đăng nhập thành công" }
                        },
                        Data = new LoginResponse()
                        {
                            Id = user.Id,
                            DisplayName = user.DisplayName,
                            VerifyStatus = user.VerifyStatus,
                            Token = token
                        }
                    };
                }
            }
        }
        public async Task ValidationLoginRequest(LoginRequest loginRequest, BaseResponse<LoginResponse> response)
        {
            if (CommonValidationRule.IsLessSixCharacter(loginRequest.Username))
            {
                response.Errors.Add("Username", "Tên đăng nhập không được để trống và phải lớn hơn 6 ký tự");
            }
            if (CommonValidationRule.IsLessSixCharacter(loginRequest.Password))
            {
                response.Errors.Add("Password", "Mật khẩu không được để trống và phải lớn hơn 6 ký tự");
            }
        }

        public string GenerateJwtToken(UserEntity user)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = configBuilder.Build();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"]
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<bool>> Active(Guid id)
        {
            var user = await userRepository.GetAsyncById(id);
            user.Status = EUserStatus.Active;
            await userRepository.UpdateAsync(user);
            return new BaseResponse<bool>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Kích hoạt tài khoản thành công",
                Succeeded = true
            };
        }
    }
}
