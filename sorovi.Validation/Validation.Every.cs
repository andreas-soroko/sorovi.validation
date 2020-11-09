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

        public static ArgumentInfo<List<TItem>> Every<TItem>(this ArgumentInfo<List<TItem>> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IList<TItem>> Every<TItem>(this ArgumentInfo<IList<TItem>> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IEnumerable<TItem>> Every<TItem>(this ArgumentInfo<IEnumerable<TItem>> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<TItem[]> Every<TItem>(this ArgumentInfo<TItem[]> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<TValue> Every<TValue, TItem>(this ArgumentInfo<TValue> arg, in Action<ArgumentInfo<TItem>> action)
            where TValue : IEnumerable<TItem> =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, TItem>

        public static ArgumentInfo<Dictionary<TKey, TItem>> Every<TKey, TItem>(this ArgumentInfo<Dictionary<TKey, TItem>> arg, in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action) =>
            EveryInternal(arg, arg.Value as IDictionary<TKey, TItem>, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>> Every<TKey, TItem>(this ArgumentInfo<IDictionary<TKey, TItem>> arg, in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<IReadOnlyDictionary<TKey, TItem>> Every<TKey, TItem>(
            this ArgumentInfo<IReadOnlyDictionary<TKey, TItem>> arg,
            in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action
        ) =>
            EveryInternal(arg, arg.Value, action);

        public static ArgumentInfo<TValue> Every<TValue, TKey, TItem>(this ArgumentInfo<TValue> arg, in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action)
            where TValue : IDictionary<TKey, TItem> =>
            EveryInternal(arg, arg.Value, action);

        #endregion

        #region <TKey, _>

        public static ArgumentInfo<Dictionary<TKey, TItem>> EveryKey<TKey, TItem>(this ArgumentInfo<Dictionary<TKey, TItem>> arg, in Action<ArgumentInfo<TKey>> action) =>
            EveryInternal(arg, arg.Value?.Keys, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>> EveryKey<TKey, TItem>(this ArgumentInfo<IDictionary<TKey, TItem>> arg, in Action<ArgumentInfo<TKey>> action) =>
            EveryInternal(arg, arg.Value?.Keys, action);

        public static ArgumentInfo<IReadOnlyDictionary<TKey, TItem>> EveryKey<TKey, TItem>(this ArgumentInfo<IReadOnlyDictionary<TKey, TItem>> arg, in Action<ArgumentInfo<TKey>> action) =>
            EveryInternal(arg, arg.Value?.Keys, action);

        public static ArgumentInfo<IEnumerable<KeyValuePair<TKey, TItem>>> EveryKey<TKey, TItem>(this ArgumentInfo<IEnumerable<KeyValuePair<TKey, TItem>>> arg, in Action<ArgumentInfo<TKey>> action) =>
            EveryInternal(arg, arg.Value?.Select(s => s.Key), action);

        public static ArgumentInfo<TValue> EveryKey<TValue, TKey, TItem>(this ArgumentInfo<TValue> arg, in Action<ArgumentInfo<TKey>> action)
            where TValue : IDictionary<TKey, TItem> =>
            EveryInternal(arg, arg.Value?.Keys, action);

        #endregion

        #region <_, TItem>

        public static ArgumentInfo<Dictionary<TKey, TItem>> EveryValue<TKey, TItem>(this ArgumentInfo<Dictionary<TKey, TItem>> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value?.Values, action);

        public static ArgumentInfo<IDictionary<TKey, TItem>> EveryValue<TKey, TItem>(this ArgumentInfo<IDictionary<TKey, TItem>> arg, in Action<ArgumentInfo<TItem>> action) =>
            EveryInternal(arg, arg.Value?.Values, action);

        public static ArgumentInfo<IEnumerable<KeyValuePair<TKey, TItem>>> EveryValue<TKey, TItem>(
            this ArgumentInfo<IEnumerable<KeyValuePair<TKey, TItem>>> arg,
            in Action<ArgumentInfo<TItem>> action
        ) =>
            EveryInternal(arg, arg.Value?.Select(s => s.Value), action);

        public static ArgumentInfo<TValue> EveryValue<TValue, TKey, TItem>(this ArgumentInfo<TValue> arg, in Action<ArgumentInfo<TItem>> action)
            where TValue : IDictionary<TKey, TItem> =>
            EveryInternal(arg, arg.Value?.Values, action);

        #endregion

        private static ArgumentInfo<TArgInfo> EveryInternal<TArgInfo, TItem>(ArgumentInfo<TArgInfo> arg, IEnumerable<TItem> list, in Action<ArgumentInfo<TItem>> action)
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

        private static ArgumentInfo<TArgInfo> EveryInternal<TArgInfo, TKey, TItem>(
            ArgumentInfo<TArgInfo> arg,
            IReadOnlyDictionary<TKey, TItem> list,
            in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action
        )
        {
            arg.IfNull();
            foreach (var o in list)
            {
                action(arg.New(o, $"{arg.MemberName}[{o.Key}]"));
            }
            return arg;
        }

        private static ArgumentInfo<TArgInfo> EveryInternal<TArgInfo, TKey, TItem>(ArgumentInfo<TArgInfo> arg, IDictionary<TKey, TItem> list, in Action<ArgumentInfo<KeyValuePair<TKey, TItem>>> action)
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