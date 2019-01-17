﻿using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;

namespace commercetools.Sdk.Client
{
    public abstract class LoginCommand<T> : Command<SignInResult<T>>
    {
        public LoginCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string AnonymousCartId { get; set; }

        public string AnonymousId { get; set; }

        public bool? UpdateProductData { get; set; }

        public AnonymousCartSignInMode AnonymousCartSignInMode { get; set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public override System.Type ResourceType => typeof(T);
    }
}
