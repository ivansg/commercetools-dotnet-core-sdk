﻿using System;
using Attribute = commercetools.Sdk.Domain.Products.Attributes.Attribute;

namespace commercetools.Sdk.Serialization
{
    internal class DateTimeAttributeMapper : DateTimeConverter<Attribute, DateTime>, ICustomJsonMapper<Attribute>
    {
    }
}