using System.Collections.Generic;

namespace sorovi.Validation.Common
{
    public static class ErrorMessage
    {
        private static Dictionary<string, string> mapping = new Dictionary<string, string>()
        {
            [ValidationType.If] = "Condition for '{0}' was not complied", // ??
            [ValidationType.IfNot] = "Condition for '{0}' was not complied", // ??

            [ValidationType.IfNull] = "Expected '{0}' not to be <null>",
            [ValidationType.IfNotNull] = "Expected '{0}' to be <null>",

            [ValidationType.IfEqualsTo] = "Expected '{0}' not to be equal to {1}",
            [ValidationType.IfNotEqualsTo] = "Expected '{0}' to be equal to {1}",

            [ValidationType.IfEmpty] = "Expected '{0}' not to be empty",
            [ValidationType.IfNotEmpty] = "Expected '{0}' to be empty",

            [ValidationType.IfNullOrWhiteSpace] = "Expected '{0}' not to be <null> or consist of only whitespace characters",
            [ValidationType.IfNotNullOrWhiteSpace] = "Expected '{0}' to be <null> or consist of only whitespace characters",

            [ValidationType.IfDefault] = "Expected '{0}' not to default",
            [ValidationType.IfNotDefault] = "Expected '{0}' to default",

            [ValidationType.IfGreaterThan] = "Expected '{0}' not to be greater than '{1}'",
            [ValidationType.IfGreaterOrEqualsThan] = "Expected '{0}' not to be greater or equals than '{1}'",

            [ValidationType.IfLowerThan] = "Expected '{0}' not to be lower than '{1}'",
            [ValidationType.IfLowerOrEqualsThan] = "Expected '{0}' not to be lower or equals than '{1}'",
        };

        public static string For(in string type, in string defaultMessage, params object[] args) =>
            string.Format(defaultMessage ?? mapping[type], args);
    }
}