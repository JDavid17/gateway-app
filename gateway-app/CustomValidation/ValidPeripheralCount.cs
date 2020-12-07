using gateway_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gateway_app.CustomValidation
{
    public class ValidPeripheralCount : ValidationAttribute
    {
        public string PeripheralErrorMessage() => "One Gateway can't have more than 10 associated devices";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gateway = (Gateway)validationContext.ObjectInstance;
            try
            {
                var cnt = gateway.Peripherals.Count();

                if (cnt >= 10)
                {
                    return new ValidationResult(PeripheralErrorMessage());
                }
            }
            catch (Exception)
            {
            }

            return ValidationResult.Success;
        }
    }
}
