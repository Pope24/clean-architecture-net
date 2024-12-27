using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Common;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Persistence.Repository;

namespace CarRentalSystem.Infrustructure
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IFinesRepository _finesRepository;
        private List<UserEntity> users = new();
        private List<VehicleEntity> vehicles = new();
        private List<TransactionEntity> transactions = new();
        private List<FinesEntity> fines = new();

        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository, IVehicleRepository vehicleRepository, ITransactionRepository transactionRepository, IFinesRepository finesRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
            _transactionRepository = transactionRepository;
            _finesRepository = finesRepository;
        }

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
            _userRepository = new UserRepository();
            _vehicleRepository = new VehicleRepository();
            _transactionRepository = new TransactionRepository();
            _finesRepository = new FinesRepository();
        }

        public async Task<BaseResponse<BookingResponse>> AddBookingAsync(BookingRequest bookingRequest)
        {
            var res = new BaseResponse<BookingResponse>();
            try
            {
                var entity = ConvertRequestToEntity(bookingRequest);
                var user = await _userRepository.GetAsyncById(entity.UserId);
                var username = user.UserName.ToUpper();
                entity.BookingNumber = username + "_" + new Random().Next(10000000, 100000000);
                entity.TextSearch = entity.BookingNumber;
                var isSuccess = await _bookingRepository.AddAsync(entity);
                if (isSuccess)
                {
                    res.StatusCode = System.Net.HttpStatusCode.OK;
                    res.Data = new BookingResponse()
                    {
                        BookingEntity = entity
                    };
                }
                else
                {
                    res.StatusCode = System.Net.HttpStatusCode.BadGateway;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return res;
        }

        public Task<IEnumerable<BookingEntity>> GetAsync()
        {
            return _bookingRepository.GetAsync();
        }

        public Task<BookingEntity> GetAsyncById(Guid id)
        {
            return _bookingRepository.GetAsyncById(id);
        }
        public BookingEntity ConvertRequestToEntity(BookingRequest bookingRequest)
        {
            return new BookingEntity()
            {
                Id = Guid.NewGuid(),
                UserId = bookingRequest.UserId,
                VehicleId = bookingRequest.VehicleId,
                CouponId = bookingRequest.CouponId,
                BookingType = bookingRequest.BookingType,
                StartDate = bookingRequest.StartDate,
                ExpectedReturnDate = bookingRequest.ExpectedReturnDate,
                ReturnAddress = bookingRequest.ReturnAddress,
                PaymentMethod = bookingRequest.PaymentMethod,
                PaymentStatus = EPaymentStatus.Unpaid,
            };
        }

        public async Task<bool> UpdateBookingAfterPaymentAsync(Guid id)
        {
            var entity = await _bookingRepository.GetAsyncById(id);
            entity.PaymentDate = DateTime.Now.ToLocalTime();
            entity.PaymentStatus = EPaymentStatus.Paid;
            return await _bookingRepository.UpdateAsync(entity);
        }
        public async Task<BasePaging<BookingHistoryResponse>> GetBookingsHistoryByUserIdAsync(Guid id, BaseFilteration filter)
        {
            var entities = await _bookingRepository.GetAllAsyncByUserId(id);
            var res = new List<BookingHistoryResponse>();

            foreach (var entity in entities.Where(e => (e.TextSearch ?? "").Contains(filter.SearchText ?? "")))
            {
                var bookingHistoryResponse = await ConvertBookingToBookingHistoryResponse(entity);
                res.Add(bookingHistoryResponse);
            }

            var paging = BasePaging<BookingHistoryResponse>.ToPagedList(res, filter.Page, 9);
            return paging;
        }
        public async Task<BookingHistoryResponse> ConvertBookingToBookingHistoryResponse(BookingEntity entity)
        {
            var user = await _userRepository.GetAsyncById(entity.UserId);
            var vehicle = await _vehicleRepository.GetAsyncById(entity.VehicleId);
            var transactionsOfBooking = await _transactionRepository.GetTransactionByBookingId(entity.Id);
            var finesOfBooking = await _finesRepository.GetFinesByBookingId(entity.Id);
            return new BookingHistoryResponse
            {
                Id = entity.Id,
                BookingNumber = entity.BookingNumber,
                UserId = entity.UserId,
                VehicleId = entity.VehicleId,
                CouponId = entity.CouponId,
                User = user,
                Vehicle = vehicle,
                BookingType = BookingConverter.ConvertBookingTypeEnumToName(entity.BookingType),
                BookingConfirmDate = entity.BookingConfirmDate,
                StartDate = entity.StartDate,
                ExpectedReturnDate = entity.ExpectedReturnDate,
                ActualReturnDate = entity.ActualReturnDate,
                RegisterReturnDate = entity.RegisterReturnDate,
                PaymentDate = entity.PaymentDate,
                ReturnAddress = entity.ReturnAddress,
                PaymentMethod = TransactionConverter.ConvertPaymentMethodEnumToName(entity.PaymentMethod),
                PaymentStatus = TransactionConverter.ConvertPaymentStatusEnumToName(entity.PaymentStatus),
                Created = entity.Created,
                TransactionHistory = transactionsOfBooking.Select(t => TransactionService.ConvertEntityToResponse(t)).OrderByDescending(e => e.Created).ToList(),
                Fines = finesOfBooking
            };
        }

        public async Task<bool> UpdateBookingAsync(BookingEntity entity)
        {
            return await _bookingRepository.UpdateAsync(entity);
        }

        public async Task<BookingHistoryResponse> GetBookingHistoryAsyncById(Guid id)
        {
            var booking = await _bookingRepository.GetAsyncById(id);
            return await ConvertBookingToBookingHistoryResponse(booking);
        }

        public async Task<BasePaging<BookingHistoryResponse>> GetBookingNeedToApprove(BaseFilteration filter)
        {
            var entities = await _bookingRepository.GetAsync();
            var res = new List<BookingHistoryResponse>();

            foreach (var entity in entities.Where(e => e.BookingConfirmDate == null && (e.TextSearch ?? "").Contains(filter.SearchText ?? "")))
            {
                var bookingHistoryResponse = await ConvertBookingToBookingHistoryResponse(entity);
                res.Add(bookingHistoryResponse);
            }

            var paging = BasePaging<BookingHistoryResponse>.ToPagedList(res, filter.Page, 9);
            return paging;
        }

        public async Task<bool> ApproveBooking(Guid bookingId)
        {
            var booking = await _bookingRepository.GetAsyncById(bookingId);
            booking.BookingConfirmDate = DateTime.Now.ToLocalTime();
            return await _bookingRepository.UpdateAsync(booking);
        }
    }
}
