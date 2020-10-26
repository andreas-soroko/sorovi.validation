/* 
 * Author: safakgur
 * Github: https://github.com/safakgur/guard
 * File: https://raw.githubusercontent.com/safakgur/guard/2a79a903895ded2c4dab7b29e7f830ba9f0a331e/src/Guard.Member.cs
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace sorovi.Validation.Common
{
    /// <summary>Represents an argument member.</summary>
    abstract class ArgumentMemberInfo
    {
        /// <summary>Cached root nodes of member trees.</summary>
        private static readonly IDictionary<MemberInfo, Node> Nodes
            = new Dictionary<MemberInfo, Node>();

        /// <summary>The lock that synchronizes access to <see cref="Nodes" />.</summary>
        private static readonly ReaderWriterLockSlim NodesLock
            = new ReaderWriterLockSlim();

        /// <summary>Returns the cached argument member for the specified lambda expression.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <typeparam name="TMember">The type of the argument member.</typeparam>
        /// <param name="lexp">
        ///     The lambda expression that specifies the argument member to get.
        /// </param>
        /// <returns>A cached, generic argument member.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="lexp" /> is not composed of <see cref="MemberExpression" /> s.
        /// </exception>
        public static ArgumentMemberInfo<T, TMember> GetMemberInfo<T, TMember>(Expression<Func<T, TMember>> lexp)
        {
            Node? node = null;

            var exp = lexp.Body;
            while (exp is MemberExpression e)
            {
                IDictionary<MemberInfo, Node> source;
                ReaderWriterLockSlim sourceLock;
                if (node is null)
                {
                    source = Nodes;
                    sourceLock = NodesLock;
                }
                else
                {
                    source = node.Owners;
                    sourceLock = node.Lock;
                }

                sourceLock.EnterUpgradeableReadLock();
                try
                {
                    if (!source.TryGetValue(e.Member, out node))
                    {
                        sourceLock.EnterWriteLock();
                        try
                        {
                            node = new Node();
                            source[e.Member] = node;
                        }
                        finally { sourceLock.ExitWriteLock(); }
                    }
                }
                finally { sourceLock.ExitUpgradeableReadLock(); }

                exp = e.Expression;
            }

            if (node is null || exp.NodeType != ExpressionType.Parameter)
            {
                var m = "The selector must be composed of member accesses.";
                throw new ArgumentException(m, nameof(lexp));
            }

            node.Lock.EnterUpgradeableReadLock();
            try
            {
                if (node.Info is ArgumentMemberInfo<T, TMember> info)
                    return info;

                node.Lock.EnterWriteLock();
                try
                {
                    info = new ArgumentMemberInfo<T, TMember>((lexp.Body as MemberExpression)!, lexp.Compile());
                    node.Info = info;
                }
                finally { node.Lock.ExitWriteLock(); }

                return info;
            }
            finally { node.Lock.ExitUpgradeableReadLock(); }
        }

        /// <summary>Represents a node in a tree of members.</summary>
        private sealed class Node
        {
            /// <summary>Initializes a new instance of the <see cref="Node" /> class.</summary>
            public Node()
            {
                Owners = new Dictionary<MemberInfo, Node>(1);
                Lock = new ReaderWriterLockSlim();
            }

            /// <summary>Gets the owners of the member that the current node represents.</summary>
            public IDictionary<MemberInfo, Node> Owners { get; }

            /// <summary>The lock that synchronizes access to the node.</summary>
            public ReaderWriterLockSlim Lock { get; }

            /// <summary>Gets or sets the argument member that the current node represents.</summary>
            public ArgumentMemberInfo? Info { get; set; }
        }
    }

    /// <summary>Represents a generic argument member.</summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    /// <typeparam name="TMember">The type of the argument member.</typeparam>
    sealed class ArgumentMemberInfo<T, TMember> : ArgumentMemberInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentMemberInfo{T, TMember}" /> class.
        /// </summary>
        /// <param name="mexp">The member expression.</param>
        /// <param name="getValue">
        ///     A function that returns the member value from the argument it belongs to.
        /// </param>
        public ArgumentMemberInfo(MemberExpression mexp, Func<T, TMember> getValue)
        {
            var memberName = mexp.ToString();

            Name = memberName.Substring(memberName.IndexOf('.') + 1);
            GetValue = getValue;
        }

        /// <summary>Gets the member name.</summary>
        public string Name { get; }

        /// <summary>
        ///     Gets a function that returns the member value from the argument it belongs to.
        /// </summary>
        public Func<T, TMember> GetValue { get; }
    }
}