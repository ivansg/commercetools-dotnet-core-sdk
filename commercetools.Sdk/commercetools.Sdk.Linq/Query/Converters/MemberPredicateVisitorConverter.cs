﻿using System.Linq.Expressions;
using commercetools.Sdk.Linq.Query.Visitors;

namespace commercetools.Sdk.Linq.Query.Converters
{
    // c.Parent.Id
    public class MemberPredicateVisitorConverter : IQueryPredicateVisitorConverter
    {
        public int Priority { get; } = 4;

        public bool CanConvert(Expression expression)
        {
            return expression.NodeType == ExpressionType.MemberAccess;
        }

        public IPredicateVisitor Convert(Expression expression, IPredicateVisitorFactory predicateVisitorFactory)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                return null;
            }

            // Id
            string currentName = memberExpression.Member.Name;
            ConstantPredicateVisitor constant = new ConstantPredicateVisitor(currentName);

            // c.Parent
            IPredicateVisitor parent = predicateVisitorFactory.Create(memberExpression.Expression);
            return new ContainerPredicateVisitor(constant, parent);
        }
    }
}
