using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baymax.Entity
{
    internal static class EntityValidation
    {
        private static readonly Dictionary<Type, Func<object, ValidationResult>> ProcessRoutines =
                new Dictionary<Type, Func<object, ValidationResult>>();

        public static bool AnyProcessRoutines()
        {
            return ProcessRoutines.Any();
        }

        public static void SetProcessRoutines(Type t, Func<object, ValidationResult> func)
        {
            ProcessRoutines[t] = func;
        }

        public static void Check(object t, ref List<ValidationResult> validationResults)
        {
            if (!ProcessRoutines.ContainsKey(t.GetType()))
            {
                return;
            }

            var validationResult = ProcessRoutines[t.GetType()].Invoke(t);

            if (validationResult != null || validationResult != ValidationResult.Success)
            {
                validationResults.Add(validationResult);
            }
        }
    }
}