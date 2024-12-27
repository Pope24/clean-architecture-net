using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.Domain.Request;

namespace CarRentalSystem.Application.Contracts.Service
{
    public interface IPaymentService
    {
        string CreatePaymentUrlByVNPay(PaymentInforRequest model, HttpContext context);
        string CreatePaymentForFines(PaymentInforRequest model, HttpContext context);
        //PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
