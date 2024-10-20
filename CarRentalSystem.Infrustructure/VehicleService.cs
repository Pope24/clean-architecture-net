using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Common;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Application.Extensions;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;

namespace CarRentalSystem.Infrustructure
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBookingRepository _bookingRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IBookingRepository bookingRepository)
        {
            this._vehicleRepository = vehicleRepository;
            this._bookingRepository = bookingRepository;
        }

        public async Task<BasePaging<VehicleResponse>> GetAsync(BaseFilteration filter)
        {
            var entities = await _vehicleRepository.GetAsync();
            var res = entities.Select(s => ConvertEntityToResponse(s)).ToList();
            var paging = BasePaging<VehicleResponse>.ToPagedList(res, filter.Page, 9);
            return paging;
        }

        public async Task<VehicleResponse> GetAsyncById(Guid id)
        {
            var entity = await _vehicleRepository.GetAsyncById(id);
            return ConvertEntityToResponse(entity);
        }
        public VehicleResponse ConvertEntityToResponse(VehicleEntity s)
        {
            var res = new VehicleResponse()
            {
                Id = s.Id,
                VehicleCode = s.VehicleCode,
                VehicleName = s.VehicleName,
                NumberOfSeats = s.VehicleEngineProperty.NumberOfSeats,
                TransmissionSystem = VehicleConverter.ConvertTransmissionSystemByEnum(s.VehicleEngineProperty.TransmissionSystem),
                Fuel = VehicleConverter.ConvertFuelByEnum(s.VehicleEngineProperty.Fuel),
                Consumption = s.VehicleEngineProperty.Consumption,
                Amenities = s.VehicleAmenityProperty.Amenities?.Select(amentity => VehicleConverter.ConvertAmenityByEnum(amentity)).ToList(),
                Description = s.Description,
                CurrentAddress = s.CurrentAddress,
                ADayRentalPrice = s.ADayRentalPrice,
                VATFee = s.AdditionalFeeProperty.VATFee,
                ReservationFee = s.AdditionalFeeProperty.ReservationFee,
                DepositFee = s.AdditionalFeeProperty.DepositFee,
                Images = s.VehicleImageProperty?.Images ?? new List<string>(),
                Created = DateTimeExtension.FormatDateTime(s.Created),
                DataExtension = s.DataExtension,
            };
            return res;
        }

        public async Task<bool> CheckAvailableVehicle(CheckAvailableRequest checking)
        {
            var bookings = await _bookingRepository.GetAsync();
            var isVehicleUnavailable = bookings
                .Where(b => b.VehicleId == checking.VehicleId)
                .Any(b => (b.BookingConfirmDate <= checking.DateOfBooking && checking.DateOfBooking <= b.ExpectedReturnDate)
                    || (b.BookingConfirmDate <= checking.DateOfReturn && checking.DateOfReturn <= b.ExpectedReturnDate));

            return !isVehicleUnavailable;
        }
    }
}
