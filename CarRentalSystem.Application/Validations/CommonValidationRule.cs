using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Validations
{
    public static class CommonValidationRule
    {
        public static bool IsNullOrEmpty(string s)
        {
            return s == null || s.Trim() == string.Empty;
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10 || phoneNumber[0] != '0')
                return false;

            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }
        public static bool IsLessSixCharacter(string s)
        {
            if (IsNullOrEmpty(s) || s.Trim().Length < 6)
            {
                return true;
            }
            return false;
        }
    }
}
