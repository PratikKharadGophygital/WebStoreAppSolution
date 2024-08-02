using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebStoreApp.Application.Common.Validation
{
    /// <summary>
    /// This class use only model validation 
    /// </summary>
    public static class ModelValidationService
    {
        /// <summary>
        /// This method use only email validate 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
