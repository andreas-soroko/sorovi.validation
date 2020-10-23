using System;
using System.Collections.Concurrent;

namespace sorovi.Validation.Exceptions
{
    public delegate Exception CreateExceptionDelegate(string type, string message, string memberName, object value);

    public static class ExceptionFactory
    {
        private static ConcurrentDictionary<Type, CreateExceptionDelegate> _exceptionDelegates = new ConcurrentDictionary<Type, CreateExceptionDelegate>();

        static ExceptionFactory()
        {
            Register<Exception>((type, message, memberName, value) => new Exception($@"[{type}] {message}"));
            Register<ValidationException>((type, message, memberName, value) => new ValidationException(type, message));
        }


        public static void Register<TException>(CreateExceptionDelegate exceptionDelegate)
            where TException : Exception
        {
            Register(typeof(TException), exceptionDelegate);
        }

        private static void Register(Type exceptionType, CreateExceptionDelegate exceptionDelegate)
        {
            if (_exceptionDelegates.ContainsKey(exceptionType)) return;
            _exceptionDelegates.TryAdd(exceptionType, exceptionDelegate);
        }

        internal static void ThrowIf<TException>(bool predicate, string type, string message, string memberName, object value)
            where TException : Exception
        {
            if (!predicate) return;

            if (!_exceptionDelegates.TryGetValue(typeof(TException), out CreateExceptionDelegate exceptionDelegate))
            {
                throw new InvalidOperationException($"Was not able to get exception delegate for exception type: {typeof(TException)}");
            }

            throw exceptionDelegate(type, message, memberName, value);
        }
    }
}