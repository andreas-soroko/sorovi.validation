namespace sorovi.Validation.Common
{
    public static class ValidationTypes
    {
        public const string If = "value_condition";
        public const string IfNot = "value_condition";

        public const string IfNull = "value_null";
        public const string IfNotNull = "value_not_null";

        public const string IfEqualsTo = "value_equals";
        public const string IfNotEqualsTo = "value_not_equals";

        public const string IfEmpty = "value_empty";
        public const string IfNotEmpty = "value_not_empty";

        public const string IfNullOrWhiteSpace = "value_null_or_white_spaces";
        public const string IfNotNullOrWhiteSpace = "value_not_null_or_white_spaces";

        public const string IfDefault = "value_default_value";
        public const string IfNotDefault = "value_not_default_value";

        public const string IfGreaterThan = "value_greater_than";
        public const string IfGreaterOrEqualsThan = "value_greater_or_equals_than";

        public const string IfLowerThan = "value_lower_than";
        public const string IfLowerOrEqualsThan = "value_lower_or_equals_than";
    }
}