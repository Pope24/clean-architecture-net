using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Common;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrustructure
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        private IBookingRepository _bookingRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBookingRepository bookingRepository)
        {
            _transactionRepository = transactionRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<BaseResponse<TransactionEntity>> AddAsync(TransactionRequest transactionRequest)
        {
            var booking = await _bookingRepository.GetAsyncById(transactionRequest.BookingId);
            var entity = ConvertRequestToEntity(transactionRequest, booking);
            entity.TextSearch = entity.TransactionNo;
            booking.TextSearch = booking.TextSearch + entity.TransactionNo;
            await _bookingRepository.UpdateAsync(booking);
            var isSuccess = await _transactionRepository.AddAsync(entity);
            if (isSuccess)
            {
                return new BaseResponse<TransactionEntity>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = entity
                };
            } else
            {
                return new BaseResponse<TransactionEntity>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Data = entity
                };
            }
        }
        public TransactionEntity ConvertRequestToEntity(TransactionRequest transactionRequest, BookingEntity booking)
        {
            return new TransactionEntity()
            {
                Status = TransactionConverter.ConvertToPaymentStatusEnum(transactionRequest.ResponseCode),
                TransactionNo = "T_" + booking.BookingNumber,
                Amount = transactionRequest.Amount / 100,
                ErrorCode = transactionRequest.ResponseCode,
                PaymentFor = transactionRequest.PaymentFor,
                PaymentMethod = booking.PaymentMethod ?? EPaymentMethod.CASH,
                UserId = booking.UserId,
                BookingId = booking.Id,
                FinesIds = transactionRequest.PaymentFor == EPaymentFor.Fines ? null : null
            };
        }
        public static TransactionResponse ConvertEntityToResponse(TransactionEntity entity)
        {
            return new TransactionResponse()
            {
                Id = entity.Id,
                Status = TransactionConverter.ConvertPaymentStatusEnumToName(entity.Status),
                ErrorCode = entity.ErrorCode,
                TransactionNo = entity.TransactionNo,
                Amount = entity.Amount,
                PaymentFor = TransactionConverter.ConvertPaymentForEnumToName(entity.PaymentFor),
                PaymentMethod = TransactionConverter.ConvertPaymentMethodEnumToName(entity.PaymentMethod),
                UserId = entity.UserId,
                BookingId = entity.BookingId,
                FinesIds = entity.FinesIds,
                Created = entity.Created,
            };
        }
    }
}
