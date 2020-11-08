using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationEvery
    {
        #region TItem[]

        public static ArgumentInfoBase<List<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfoBase<List<TItem>, TEx> arg, in Action<ArgumentInfoBase<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfoBase<IList<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfoBase<IList<TItem>, TEx> arg, in Action<ArgumentInfoBase<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfoBase<IEnumerable<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfoBase<IEnumerable<TItem>, TEx> arg, in Action<ArgumentInfoBase<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfoBase<TItem[], TEx> Every<TItem, TEx>(this ArgumentInfoBase<TItem[], TEx> arg, in Action<ArgumentInfoBase<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, TItem>

        public static ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> Every<TKey, TItem, TEx>(
            this ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<KeyValuePair<TKey, TItem>, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> Every<TKey, TItem, TEx>(
            this ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<KeyValuePair<TKey, TItem>, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, _>

        public static ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> EveryKey<TKey, TItem, TEx>(
            this ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<TKey, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Keys, action);

        public static ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> EveryKey<TKey, TItem, TEx>(
            this ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<TKey, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Keys, action);

        #endregion

        #region <_, TItem>

        public static ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> EveryValue<TKey, TItem, TEx>(
            this ArgumentInfoBase<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<TItem, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Values, action);

        public static ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> EveryValue<TKey, TItem, TEx>(
            this ArgumentInfoBase<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfoBase<TItem, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Values, action);

        #endregion

        private static ArgumentInfoBase<TArgInfo, TEx> EveryInternal<TArgInfo, TItem, TEx>(
            ArgumentInfoBase<TArgInfo, TEx> arg,
            IEnumerable<TItem> list,
            in Action<ArgumentInfoBase<TItem, TEx>> action
        )
            where TEx : Delegate
        {
            arg.IfNull();
            var count = 0;

            foreach (var o in list)
            {
                action(arg.New(o, $"{arg.MemberName}[{count}]"));
                count++;
            }
            return arg;
        }

        private static ArgumentInfoBase<TArgInfo, TEx> EveryInternal<TArgInfo, TKey, TItem, TEx>(
            ArgumentInfoBase<TArgInfo, TEx> arg,
            IDictionary<TKey, TItem> list,
            in Action<ArgumentInfoBase<KeyValuePair<TKey, TItem>, TEx>> action
        )
            where TEx : Delegate
        {
            arg.IfNull();
            foreach (var o in list)
            {
                action(arg.New(o, $"{arg.MemberName}[{o.Key}]"));
            }
            return arg;
        }
    }
}