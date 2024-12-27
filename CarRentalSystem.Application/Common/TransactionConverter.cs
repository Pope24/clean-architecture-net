using CarRentalSystem.Domain.Enum;

namespace CarRentalSystem.Application.Common
{
    public static class TransactionConverter
    {
        public static EPaymentStatus ConvertToPaymentStatusEnum(string code)
        {
            switch (code)
            {
                case "00":
                    return EPaymentStatus.Paid;
                //SCAM - NGHI NGO
                case "07":
                    return EPaymentStatus.Pending;
                //KHONG THANH CONG
                case "10":
                case "11":
                case "12":
                    return EPaymentStatus.Failed;
                //HUY GIAO DICH
                case "24":
                    return EPaymentStatus.Canceled;
                default:
                    return EPaymentStatus.Unpaid;
            }
        }
        public static string ConvertPaymentStatusEnumToName(EPaymentStatus? status)
        {
            switch (status)
            {
                case EPaymentStatus.Paid:
                    return "Đã thanh toán";
                case EPaymentStatus.Pending:
                    return "Chờ phê duyệt";
                case EPaymentStatus.Failed:
                    return "Giao dịch lỗi";
                case EPaymentStatus.Unpaid:
                    return "Chưa thanh toán";
                case EPaymentStatus.PartiallyPaid:
                    return "Thanh toán một phần";
                case EPaymentStatus.Canceled:
                    return "Hủy giao dịch";
            }
            return "Chưa xác minh";
        }
        public static string ConvertPaymentMethodEnumToName(EPaymentMethod? method)
        {
            switch (method)
            {
                case EPaymentMethod.CASH:
                    return "Tại cửa hàng";
                case EPaymentMethod.VNPAY:
                    return "VNPay";
                case EPaymentMethod.PAYPAL:
                    return "PayPal";
            }
            return "Chưa xác minh";
        }
        public static string ConvertPaymentForEnumToName(EPaymentFor paymentFor)
        {
            switch (paymentFor)
            {
                case EPaymentFor.Booking:
                    return "Thuê xe";
                case EPaymentFor.Fines:
                    return "Phí phạt";
            }
            return "Chưa xác minh";
        }
    }
}
