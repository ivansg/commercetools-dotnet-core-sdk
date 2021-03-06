﻿using System;
using System.Linq.Expressions;

namespace commercetools.Sdk.Domain.ProductProjections
{
    public class FilterFacet<T> : Facet<T>
    {
        public FilterFacet(Expression<Func<T, bool>> expression)
        {
            this.Expression = expression;
        }
    }
}