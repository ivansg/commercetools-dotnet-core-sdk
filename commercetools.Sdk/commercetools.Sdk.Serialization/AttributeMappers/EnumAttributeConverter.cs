﻿using System;
using System.Collections.Generic;
using System.Text;
using commercetools.Sdk.Domain;
using Newtonsoft.Json.Linq;
using Type = System.Type;

namespace commercetools.Sdk.Serialization
{
    public class EnumAttributeConverter : EnumConverter<Domain.Attribute, EnumAttribute>,  ICustomJsonMapper<Domain.Attribute>
    {
    }
}
