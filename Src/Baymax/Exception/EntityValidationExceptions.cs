using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baymax.Exception
{
    public class EntityValidationException : ValidationException
    {
        public EntityValidationException(IEnumerable<ValidationResult> validationResults)
        {
            Exceptions = validationResults.Select(a => new ValidationException(a, null, null)).ToList();
        }

        public List<ValidationException> Exceptions { get; }
    }
}