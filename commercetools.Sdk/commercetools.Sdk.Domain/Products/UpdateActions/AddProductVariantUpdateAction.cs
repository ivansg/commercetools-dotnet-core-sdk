﻿using System.Collections.Generic;
using commercetools.Sdk.Domain.Products.Attributes;

namespace commercetools.Sdk.Domain.Products.UpdateActions
{
    public class AddProductVariantUpdateAction : UpdateAction<Product>
    {
        public string Action => "addVariant";
        public string Sku { get; set; }
        public string Key { get; set; }
        public List<Price> Prices { get; set; }
        public List<Image> Images { get; set; }
        public List<Attribute> Attributes { get; set; }
        public bool Staged { get; set; }
    }
}