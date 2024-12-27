using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IAppraiseService
    {
        Task<BasePaging<BookingHistoryResponse>> GetAllRequestReturn(BaseFilteration filter);
    }
}
