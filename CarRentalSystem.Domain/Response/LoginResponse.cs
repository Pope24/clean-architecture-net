using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public string VerifyStatus { get; set; }
        public string Avatar { get; set; } = "https://i.pinimg.com/736x/c6/e5/65/c6e56503cfdd87da299f72dc416023d4.jpg";
        public string Token { get; set; }
    }
}
