using System.Collections.Generic;

namespace sorovi.Validation.Common
{
    public static class ErrorMessage
    {
        private static Dictionary<string, string> mapping = new Dictionary<string, string>()
        {
            [ValidationTypes.ValueIf] = "Condition for '{0}' was not complied", // ??
            [ValidationTypes.ValueIfNot] = "Condition for '{0}' was not complied", // ??

            [ValidationTypes.ValueNull] = "Expected '{0}' not to be <null>",
            [ValidationTypes.ValueNotNull] = "Expected '{0}' to be <null>",

            [ValidationTypes.ValueEquals] = "Expected '{0}' not to be equal to {1}",
            [ValidationTypes.ValueNotEquals] = "Expected '{0}' to be equal to {1}",

            [ValidationTypes.ValueEmpty] = "Expected '{0}' not to be empty",
            [ValidationTypes.ValueNotEmpty] = "Expected '{0}' to be empty",

            [ValidationTypes.ValueNullOrWhiteSpace] = "Expected '{0}' not to be <null> or consist of only whitespace characters",
            [ValidationTypes.ValueNotNullOrWhiteSpace] = "Expected '{0}' to be <null> or consist of only whitespace characters",

            [ValidationTypes.ValueDefaultValue] = "Expected '{0}' not to default",
            [ValidationTypes.ValueNotDefaultValue] = "Expected '{0}' to default",

            [ValidationTypes.ValueGreaterThan] = "Expected '{0}' not to be greater than '{1}'",
            [ValidationTypes.ValueGreaterOrEqualsThan] = "Expected '{0}' not to be greater or equals than '{1}'",

            [ValidationTypes.ValueLowerThan] = "Expected '{0}' not to be lower than '{1}'",
            [ValidationTypes.ValueLowerOrEqualsThan] = "Expected '{0}' not to be lower or equals than '{1}'",
        };

        public static string For(in string type, in string defaultMessage, params object[] args) =>
            string.Format(defaultMessage ?? mapping[type], args);
    }
}