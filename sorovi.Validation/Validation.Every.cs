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

        public static ArgumentInfo<List<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfo<List<TItem>, TEx> arg, in Action<ArgumentInfo<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IList<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfo<IList<TItem>, TEx> arg, in Action<ArgumentInfo<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IEnumerable<TItem>, TEx> Every<TItem, TEx>(this ArgumentInfo<IEnumerable<TItem>, TEx> arg, in Action<ArgumentInfo<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<TItem[], TEx> Every<TItem, TEx>(this ArgumentInfo<TItem[], TEx> arg, in Action<ArgumentInfo<TItem, TEx>> action)
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, TItem>

        public static ArgumentInfo<Dictionary<TKey, TItem>, TEx> Every<TKey, TItem, TEx>(
            this ArgumentInfo<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<KeyValuePair<TKey, TItem>, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>, TEx> Every<TKey, TItem, TEx>(
            this ArgumentInfo<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<KeyValuePair<TKey, TItem>, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, _>

        public static ArgumentInfo<Dictionary<TKey, TItem>, TEx> EveryKey<TKey, TItem, TEx>(
            this ArgumentInfo<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<TKey, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Keys, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>, TEx> EveryKey<TKey, TItem, TEx>(
            this ArgumentInfo<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<TKey, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Keys, action);

        #endregion

        #region <_, TItem>

        public static ArgumentInfo<Dictionary<TKey, TItem>, TEx> EveryValue<TKey, TItem, TEx>(
            this ArgumentInfo<Dictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<TItem, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Values, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>, TEx> EveryValue<TKey, TItem, TEx>(
            this ArgumentInfo<IDictionary<TKey, TItem>, TEx> arg,
            in Action<ArgumentInfo<TItem, TEx>> action
        )
            where TEx : Delegate =>
            EveryInternal(arg, arg.Value?.Values, action);

        #endregion

        private static ArgumentInfo<TArgInfo, TEx> EveryInternal<TArgInfo, TItem, TEx>(
            ArgumentInfo<TArgInfo, TEx> arg,
            IEnumerable<TItem> list,
            in Action<ArgumentInfo<TItem, TEx>> action
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

        private static ArgumentInfo<TArgInfo, TEx> EveryInternal<TArgInfo, TKey, TItem, TEx>(
            ArgumentInfo<TArgInfo, TEx> arg,
            IDictionary<TKey, TItem> list,
            in Action<ArgumentInfo<KeyValuePair<TKey, TItem>, TEx>> action
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