using gateway_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gateway_app.CustomValidation
{
    public class ValidIpv4 : ValidationAttribute
    {
        //Error Message
        public string Ipv4ErrorMessage() => "IP Address not valid, please enter a valid Ipv4 address";

        //Custom Validator
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gateway = (Gateway)validationContext.ObjectInstance;
            var ipv4 = gateway.Ipv4;

            Regex regex = new Regex("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$");

            if (!regex.IsMatch(ipv4))
            {
                return new ValidationResult(Ipv4ErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
