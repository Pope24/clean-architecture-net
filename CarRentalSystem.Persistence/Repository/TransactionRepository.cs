using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<bool> AddAsync(TransactionEntity entity)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.Transactions.AddAsync(entity);
                var res = await context.SaveChangesAsync();
                return res > 0 ? true : false;
            }
        }

        public async Task<List<TransactionEntity>> GetAllAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var transactions = await context.Transactions.ToListAsync();
                return transactions;
            }
        }

        public async Task<List<TransactionEntity>> GetTransactionByBookingId(Guid bookingId)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Transactions.Where(t => t.BookingId == bookingId).ToListAsync();
            }
        }
    }
}
