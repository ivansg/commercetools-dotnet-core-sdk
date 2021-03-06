﻿using System.Linq.Expressions;
using commercetools.Sdk.Linq.Discount.Visitors;

namespace commercetools.Sdk.Linq.Discount.Converters
{
    public class BinaryLogicalPredicateVisitorConverter : IDiscountPredicateVisitorConverter
    {
        public int Priority => 4;

        public bool CanConvert(Expression expression)
        {
            return Mapping.LogicalOperators.ContainsKey(expression.NodeType);
        }

        public IPredicateVisitor Convert(Expression expression, IPredicateVisitorFactory predicateVisitorFactory)
        {
            BinaryExpression binaryExpression = expression as BinaryExpression;
            if (binaryExpression == null)
            {
                return null;
            }

            IPredicateVisitor predicateVisitorLeft = predicateVisitorFactory.Create(binaryExpression.Left);
            IPredicateVisitor predicateVisitorRight = predicateVisitorFactory.Create(binaryExpression.Right);
            string operatorSign = Mapping.GetOperator(expression.NodeType, Mapping.LogicalOperators);
            return new BinaryLogicalPredicateVisitor(predicateVisitorLeft, operatorSign, predicateVisitorRight);
        }
    }
}