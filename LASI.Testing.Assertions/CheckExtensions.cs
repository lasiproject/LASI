using System;
using NFluent;
using NFluent.Extensibility;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LASI.Testing.Assertions
{
    public static partial class CheckExtensions
    {
        public static ICheckLink<ICheck<T>> IsSameReferenceAs<T>(this ICheck<T> check, T expected) => check.IsSameReferenceAs(expected);

        public static ICheck<TValue> HasMember<T, TValue>(this ICheck<T> check, Expression<Func<T, TValue>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new FluentCheckException($"Expression given to {nameof(HasMember)} constraint is not a valid property access expression");
            }
            var checker = ExtensibilityHelper.ExtractChecker(check);
            var property = (PropertyInfo)((MemberExpression)expression.Body).Member;
            var value = (TValue)property.GetValue(checker.Value);

            return Check.That(value);
        }
    }
}