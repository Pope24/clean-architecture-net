using CarRentalSystem.Domain.Enum;

namespace CarRentalSystem.Application.Common
{
    public static class UserConverter
    {
        public static string ConvertRoleByEnum(ERole role)
        {
            switch (role)
            {
                case ERole.Customer:
                    return "CUSTOMER";
                case ERole.Appraiser:
                    return "APPRAISER";
                case ERole.Admin:
                    return "ADMIN";
            }
            return "CUSTOMER";
        }
        public static string ConvertVerifyStatusByEnum(EVerifyStatus verifyStatus)
        {
            switch (verifyStatus)
            {
                case EVerifyStatus.Pending:
                    return "Chờ phê duyệt";
                case EVerifyStatus.Verified:
                    return "Đã xác minh";
                case EVerifyStatus.Unverified:
                    return "Chưa xác minh";
                default:
                    return "Không rõ";
            }
        }
        public static string ConvertStatusByEnum(EUserStatus status)
        {
            switch (status)
            {
                case EUserStatus.Active:
                    return "Đã kích hoạt";
                case EUserStatus.Inactive:
                    return "Chưa kích hoạt";
                default:
                    return "Không rõ";
            }
        }
    }
}