using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using FluentValidation.Results;

namespace Ecommerce.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {

        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() 
        : base("Se presentaron uno o mas errores en la applicacion")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy((failure)=>failure.PropertyName, (error)=>error.ErrorMessage)
                             .ToDictionary((failureGroup)=>failureGroup.Key, (failureGroup)=>failureGroup.ToArray());
        }

    }
}