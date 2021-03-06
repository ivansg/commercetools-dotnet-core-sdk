﻿using System.ComponentModel.DataAnnotations;

namespace commercetools.Sdk.Domain.Categories.UpdateActions
{
    public class ChangeOrderHintUpdateAction : UpdateAction<Category>
    {
        public string Action => "changeOrderHint";
        [Required]
        public string OrderHint { get; set; }
    }
}