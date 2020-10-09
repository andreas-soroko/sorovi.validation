using System;

namespace sorovi.Validation.Exceptions
{
    public class ValidationException : Exception
    {
        public string Type { get; }

        public ValidationException(string type, string message)
            : base(message)
        {
            Type = type;
        }
    }
}