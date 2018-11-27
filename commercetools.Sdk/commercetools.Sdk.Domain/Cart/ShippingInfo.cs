﻿using System.Collections.Generic;

namespace commercetools.Sdk.Domain
{
    public class ShippingInfo
    {
        public string ShippingMethodName { get; set; }
        public CentPrecisionMoney Price { get; set; }
        public ShippingRate ShippingRate { get; set; }
        public TaxedItemPrice TaxedPrice { get; set; }
        public TaxRate TaxRate { get; set; }
        public Reference<TaxCategory> TaxCategory { get; set; }
        public Reference<ShippingMethod> ShippingMethod { get; set; }
        public List<Delivery> Deliveries { get; set; }
        public DiscountedLineItemPrice DiscountedPrice { get; set; }
        public ShippingMethodState ShippingMethodState { get; set; }
    }
}