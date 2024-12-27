using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Repository
{
    public interface ITransactionRepository
    {
        Task<bool> AddAsync(TransactionEntity entity);
        Task<List<TransactionEntity>> GetAllAsync();
        Task<List<TransactionEntity>> GetTransactionByBookingId(Guid bookingId);
    }
}
