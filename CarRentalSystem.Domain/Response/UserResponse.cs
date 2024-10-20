using CarRentalSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string VerifyStatus { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Created { get; set; }
    }
}
