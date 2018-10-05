using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baymax.Entity
{
    internal class EntityValidation
    {
        private static readonly Dictionary<Type, Func<object, ValidationResult>> _processRoutines =
                new Dictionary<Type, Func<object, ValidationResult>>();

        public static bool AnyProcessRoutines()
        {
            return _processRoutines.Any();
        }

        public static void SetProcessRoutines(Type t, Func<object, ValidationResult> func)
        {
            _processRoutines[t] = func;
        }

        public static void Check(object t, ref List<ValidationResult> validationResults)
        {
            if (!_processRoutines.ContainsKey(t.GetType()))
            {
                return;
            }

            var validationResult = _processRoutines[t.GetType()].Invoke(t);

            if (validationResult != null || validationResult != ValidationResult.Success)
            {
                validationResults.Add(validationResult);
            }
        }
    }
}