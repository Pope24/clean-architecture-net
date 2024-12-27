using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface ITransactionService
    {
        Task<BaseResponse<TransactionEntity>> AddAsync(TransactionRequest transactionRequest);
    }
}
