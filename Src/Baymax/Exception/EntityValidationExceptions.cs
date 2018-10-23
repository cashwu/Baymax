using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Baymax.Exception
{
    [Serializable]
    public class EntityValidationException : ValidationException
    {
        protected EntityValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
                : base(serializationInfo, streamingContext)
        {
        }

        public EntityValidationException(IEnumerable<ValidationResult> validationResults)
        {
            Exceptions = validationResults.Select(a => new ValidationException(a, null, null)).ToList();
        }

        public List<ValidationException> Exceptions { get; }
    }
}