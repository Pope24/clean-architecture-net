using CarRentalSystem.Application.Contracts.Repository;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Persistence.Repository;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CarRentalSystem.Infrustructure
{
    public class EmailSenderService : IEmailSenderService
    {
        private IUserRepository _userRepository = new UserRepository();
        public void SendEmail(UserEntity user, EContentEmail contentEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Car Rental Sytem", "chinhlvde170423@fpt.edu.vn"));
            message.To.Add(new MailboxAddress(user.DisplayName, user.Email));
            message.Subject = GetSubjectEmai(contentEmail);
            message.Body = new TextPart("plain")
            {
                Text = GetContentEmail(user, contentEmail)
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("chinhlvde170423@fpt.edu.vn", "q v w n y g y j q v p p c s g i");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        public string GetContentEmail(UserEntity user, EContentEmail contentEmail)
        {
            var userToSend = _userRepository.GetAsyncByEmail(user.Email);
            var content = "";
            switch (contentEmail)
            {
                case EContentEmail.AccountActive:
                    content = string.Format("Link kích hoạt tài khoản: http://localhost:3000/active?UserId={0}", user.Id);
                    break;
                default:
                    break;
            }
            return content;
        }
        public string GetSubjectEmai(EContentEmail contentEmail)
        {
            var subject = "";
            switch (contentEmail)
            {
                case EContentEmail.AccountActive:
                    subject = "Kích hoạt tài khoản";
                    break;
                default:
                    break;
            }
            return subject;
        }
    }
}
