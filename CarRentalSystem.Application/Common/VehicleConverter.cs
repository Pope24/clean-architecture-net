using CarRentalSystem.Domain.Enum;

namespace CarRentalSystem.Application.Common
{
    public static class VehicleConverter
    {
        public static string ConvertTransmissionSystemByEnum(ETransmissionSystem transmissionSystem)
        {
            switch (transmissionSystem)
            {
                case ETransmissionSystem.Manual:
                    return "Thủ công";
                case ETransmissionSystem.Automatic:
                    return "Tự động";
            }
            return "";
        }
        public static string ConvertFuelByEnum(EFuel fuel)
        {
            switch (fuel)
            {
                case EFuel.Oil:
                    return "Dầu";
                case EFuel.Petrol:
                    return "Xăng";
            }
            return "";
        }
        public static string ConvertAmenityByEnum(EAmenityVehicle amenityVehicle)
        {
            switch (amenityVehicle)
            {
                case EAmenityVehicle.Map:
                    return "Bản đồ";
                case EAmenityVehicle.Bluetooth:
                    return "Bluetooth";
                case EAmenityVehicle.Camera360:
                    return "Camera 360";
                case EAmenityVehicle.CameraDash:
                    return "Camera hành trình";
                case EAmenityVehicle.CameraCurb:
                    return "Camera lề đường";
                case EAmenityVehicle.CameraReverse:
                    return "Camera lùi";
                case EAmenityVehicle.TireSensor:
                    return "Cảm biến lốp";
                case EAmenityVehicle.CollisionSensor:
                    return "Cảm biến va chạm";
                case EAmenityVehicle.SpeedWarning:
                    return "Cảm biến tốc độ";
                case EAmenityVehicle.Sunroof:
                    return "Cửa sổ trời";
                case EAmenityVehicle.GPSNavigation:
                    return "Định vị GPS";
                case EAmenityVehicle.USBSlot:
                    return "Khe cắm USB";
                case EAmenityVehicle.SpareTire:
                    return "Lốp dự phòng";
                case EAmenityVehicle.Screen:
                    return "Màn hình";
                case EAmenityVehicle.ETC:
                    return "ETC";
                case EAmenityVehicle.Airbag:
                    return "ETC";
            }
            return "";
        }
    }
}
