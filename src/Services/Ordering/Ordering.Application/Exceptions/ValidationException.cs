using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("one or more validation occurred")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(fg => fg.Key, fg => fg.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }

    }
}
