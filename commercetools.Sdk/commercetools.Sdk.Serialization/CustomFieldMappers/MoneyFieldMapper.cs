﻿using commercetools.Sdk.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Type = System.Type;

namespace commercetools.Sdk.Serialization
{
    public class MoneyFieldMapper : MoneyConverter<Fields, Money>, ICustomJsonMapper<Fields>
    {
    }
}
