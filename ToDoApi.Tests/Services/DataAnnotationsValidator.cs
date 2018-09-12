using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Tests.UnitTests.Services
{
    public class DataAnnotationsValidator
    {
        public static bool TryValidate(object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                obj, context, results,
                validateAllProperties: true
            );
        }
    }

}