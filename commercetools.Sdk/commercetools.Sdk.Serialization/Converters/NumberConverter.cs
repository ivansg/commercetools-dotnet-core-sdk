﻿using System;
using System.Collections.Generic;
using System.Text;
using commercetools.Sdk.Domain;
using Newtonsoft.Json.Linq;
using Type = System.Type;

namespace commercetools.Sdk.Serialization
{
    public class NumberConverter<T, S> : ICustomConverter<T>
    {
        public int Priority => 4;

        public Type Type => typeof(S);

        public bool CanConvert(JToken property)
        {
            if (property?.Type == JTokenType.Integer)
            {
                return true;
            }
            return false;
        }
    }
}
