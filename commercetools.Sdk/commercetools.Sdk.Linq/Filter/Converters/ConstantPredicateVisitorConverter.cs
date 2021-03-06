﻿using System.Linq.Expressions;
using commercetools.Sdk.Linq.Filter.Visitors;

namespace commercetools.Sdk.Linq.Filter.Converters
{
    public class ConstantPredicateVisitorConverter : IFilterPredicateVisitorConverter
    {
        public int Priority { get; } = 4;

        public bool CanConvert(Expression expression)
        {
            return expression.NodeType == ExpressionType.Constant || IsVariable(expression);
        }

        public IPredicateVisitor Convert(Expression expression, IPredicateVisitorFactory predicateVisitorFactory)
        {
            // The value null can be passed in Range method and should be rendered as *.
            if (expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value == null)
            {
                return new ConstantPredicateVisitor("*");
            }

            if (expression.NodeType == ExpressionType.Constant)
            {
                return new ConstantPredicateVisitor(expression.ToString());
            }

            MemberExpression memberExpression = expression as MemberExpression;
            var compiledValue = Expression.Lambda(expression, null).Compile().DynamicInvoke(null).ToString();
            if (memberExpression?.Type == typeof(string))
            {
                compiledValue = compiledValue.WrapInQuotes();
            }

            return new ConstantPredicateVisitor(compiledValue);
        }

        private static bool IsVariable(Expression expression)
        {
            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                return false;
            }

            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression?.Expression.NodeType == ExpressionType.Constant)
            {
                return true;
            }

            return false;
        }
    }
}
