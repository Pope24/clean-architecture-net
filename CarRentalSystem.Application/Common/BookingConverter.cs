using CarRentalSystem.Domain.Enum;

namespace CarRentalSystem.Application.Common
{
    public static class BookingConverter
    {
        public static string ConvertBookingTypeEnumToName(EBookingType type)
        {
            switch (type)
            {
                case EBookingType.ReturnOnSite:
                    return "Lấy tận nơi";
                case EBookingType.ReturnInStore:
                    return "Tại cửa hàng";
            }
            return "Không xác định";
        }
    }
}
