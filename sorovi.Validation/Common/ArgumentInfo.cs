using System.Collections.Generic;

namespace sorovi.Validation
{
    public readonly struct ArgumentInfo<TValue>
    {
        public TValue Value { get; }
        internal string MemberName { get; }

        public ArgumentInfo(in TValue value, in string memberName)
        {
            Value = value;
            MemberName = memberName;
        }

        public static implicit operator TValue(ArgumentInfo<TValue> arg)
        {
            return arg.Value;
        }
    }
}