using System.Collections.Generic;

namespace sorovi.Validation.Common
{
    public static class ErrorMessage
    {
        private static Dictionary<string, string> mapping = new Dictionary<string, string>()
        {
            [ValidationTypes.If] = "Condition for '{0}' was not complied", // ??
            [ValidationTypes.IfNot] = "Condition for '{0}' was not complied", // ??

            [ValidationTypes.IfNull] = "Expected '{0}' not to be <null>",
            [ValidationTypes.IfNotNull] = "Expected '{0}' to be <null>",

            [ValidationTypes.IfEqualsTo] = "Expected '{0}' not to be equal to {1}",
            [ValidationTypes.IfNotEqualsTo] = "Expected '{0}' to be equal to {1}",

            [ValidationTypes.IfEmpty] = "Expected '{0}' not to be empty",
            [ValidationTypes.IfNotEmpty] = "Expected '{0}' to be empty",

            [ValidationTypes.IfNullOrWhiteSpace] = "Expected '{0}' not to be <null> or consist of only whitespace characters",
            [ValidationTypes.IfNotNullOrWhiteSpace] = "Expected '{0}' to be <null> or consist of only whitespace characters",

            [ValidationTypes.IfDefault] = "Expected '{0}' not to default",
            [ValidationTypes.IfNotDefault] = "Expected '{0}' to default",

            [ValidationTypes.IfGreaterThan] = "Expected '{0}' not to be greater than '{1}'",
            [ValidationTypes.IfGreaterOrEqualsThan] = "Expected '{0}' not to be greater or equals than '{1}'",

            [ValidationTypes.IfLowerThan] = "Expected '{0}' not to be lower than '{1}'",
            [ValidationTypes.IfLowerOrEqualsThan] = "Expected '{0}' not to be lower or equals than '{1}'",
        };

        public static string For(in string type, in string defaultMessage, params object[] args) =>
            string.Format(defaultMessage ?? mapping[type], args);
    }
}