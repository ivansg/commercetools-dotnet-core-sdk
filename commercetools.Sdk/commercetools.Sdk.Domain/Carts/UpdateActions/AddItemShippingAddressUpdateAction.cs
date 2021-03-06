﻿namespace commercetools.Sdk.Domain.Carts.UpdateActions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using commercetools.Sdk.Domain.Validation.Attributes;

    public class AddItemShippingAddressUpdateAction : UpdateAction<Cart>
    {
        public string Action => "addItemShippingAddress";
        [Required]
        public Address Address { get; set; }
    }
}