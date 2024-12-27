using CarRentalSystem.Domain.Enum;
using Crs.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Mobile {  get; set; }
        public string PasswordHash { get; set; }
        public ERole Role { get; set; }
        public ESignInMethod SignInMethod { get; set; }
        public EVerifyStatus VerifyStatus { get; set; }
        public string Address { get; set; }
        public EUserStatus Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now.ToLocalTime();
        public string TextSearch { get; set; }
        public LegacyDataExtension DataExtension { get; set; }
        public ICollection<BookingEntity> Bookings { get;} = new List<BookingEntity>();
    }
}
