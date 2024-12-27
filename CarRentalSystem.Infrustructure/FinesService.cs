using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrustructure
{
    public class FinesService : IFinesService
    {
        private readonly IFinesRepository _finesRepository;

        public FinesService(IFinesRepository finesRepository)
        {
            _finesRepository = finesRepository;
        }

        public FinesService()
        {
            _finesRepository = new FinesRepository();
        }

        public async Task<bool> AddFinesRange(List<FinesRequest> finesRequests)
        {
            var convertRes = finesRequests.Select(f => ConvertRequestToEntity(f)).ToList();
            return await _finesRepository.AddFinesRange(convertRes);

        }

        public async Task<List<FinesEntity>> GetAllFines()
        {
            return await _finesRepository.GetAllFines();
        }

        public async Task<List<FinesEntity>> GetFinesByBookingId(Guid bookingId)
        {
            return await _finesRepository.GetFinesByBookingId(bookingId);
        }
        public FinesEntity ConvertRequestToEntity(FinesRequest finesRequest)
        {
            return new FinesEntity()
            {
                BookingId = finesRequest.BookingId,
                Amount = finesRequest.Amount,
                Description = finesRequest.Description,
                FinesReason = finesRequest.FinesReason,
                FinesDate = DateTime.Now.ToLocalTime(),
                PaymentStatus = EPaymentStatus.Unpaid,
            };
        }

        public async Task<bool> ChangePaymentStatus(Guid bookingId, EPaymentStatus paymentStatus)
        {
            var finesOfBooking = await _finesRepository.GetFinesByBookingId(bookingId);
            if (!finesOfBooking.Any())
            {
                return false;
            }
            foreach (var item in finesOfBooking)
            {
                item.PaymentStatus = paymentStatus;
                item.PaymentDate = DateTime.Now.ToLocalTime();
            }
            return await _finesRepository.UpdateFinesRange(finesOfBooking);
        }

        public async Task<bool> AddFines(FinesRequest finesEntity)
        {
            var convertRes = ConvertRequestToEntity(finesEntity);
            return await _finesRepository.AddFines(convertRes);
        }
    }
}
