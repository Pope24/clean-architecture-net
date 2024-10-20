using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;

namespace CarRentalSystem.Infrustructure
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<BookingResponse>> AddBookingAsync(BookingRequest bookingRequest)
        {
            var res = new BaseResponse<BookingResponse>();
            try
            {
                var entity = ConvertRequestToEntity(bookingRequest);
                var username = _userRepository.GetAsyncById(entity.UserId).Result.UserName.ToUpper();
                entity.BookingNumber = username + "_" + new Random().Next(10000000, 100000000);
                entity.TextSearch = entity.BookingNumber;
                //var isSuccess = await _bookingRepository.AddAsync(entity);
                if (true)
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
                PaymentStatus = Domain.Enum.EPaymentStatus.Unpaid,
            };
        }
    }
}
