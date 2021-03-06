using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.ShippingMethods;
using commercetools.Sdk.Domain.Zones;
using commercetools.Sdk.HttpApi.IntegrationTests.TaxCategories;
using commercetools.Sdk.HttpApi.IntegrationTests.Zones;

namespace commercetools.Sdk.HttpApi.IntegrationTests.ShippingMethods
{
    public class ShippingMethodsFixture : ClientFixture, IDisposable
    {
        public List<ShippingMethod> ShippingMethodsToDelete { get; private set; }

        private readonly TaxCategoryFixture taxCategoryFixture;
        private readonly ZonesFixture zonesFixture;

        public ShippingMethodsFixture() : base()
        {
            this.ShippingMethodsToDelete = new List<ShippingMethod>();
            this.taxCategoryFixture = new TaxCategoryFixture();
            this.zonesFixture = new ZonesFixture();
        }

        public void Dispose()
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            this.ShippingMethodsToDelete.Reverse();
            foreach (ShippingMethod shippingMethod in this.ShippingMethodsToDelete)
            {
                ShippingMethod deletedShippingMethod = commerceToolsClient
                    .ExecuteAsync(new DeleteByIdCommand<ShippingMethod>(new Guid(shippingMethod.Id),
                        shippingMethod.Version)).Result;
            }

            this.taxCategoryFixture.Dispose();
            this.zonesFixture.Dispose();
        }

        /// <summary>
        /// Get Shipping Method Draft
        /// </summary>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ShippingMethodDraft GetShippingMethodDraft(string country = null, string state = null)
        {
            int ran = TestingUtility.RandomInt();
            string shippingCountry = country ?? TestingUtility.GetRandomEuropeCountry();
            string shippingState = state ?? $"{shippingCountry}_State_{TestingUtility.RandomInt()}";

            TaxCategory taxCategory = this.CreateNewTaxCategory(shippingCountry, shippingState);
            ZoneRateDraft zoneRateDraft = this.GetNewZoneRateDraft(shippingCountry, shippingState);
            ShippingMethodDraft shippingMethodDraft = new ShippingMethodDraft()
            {
                Name = $"Dhl_{ran}",
                Key = $"Dhl_key_{ran}",
                TaxCategory = new ResourceIdentifier<TaxCategory>
                {
                    Key = taxCategory.Key
                },
                ZoneRates = new List<ZoneRateDraft>()
                {
                    zoneRateDraft
                },
                Description = "DHL"
            };
            return shippingMethodDraft;
        }

        private ZoneRateDraft GetNewZoneRateDraft(string country = null, string state = null)
        {
            Zone zone = this.CreateNewZone(country, state);
            ShippingRate shippingRate = this.GetShippingRate();
            ZoneRateDraft zoneRateDraft = new ZoneRateDraft()
            {
                Zone = new ResourceIdentifier<Zone>
                {
                    Key = zone.Key
                },
                ShippingRates = new List<ShippingRate>() {shippingRate}
            };
            return zoneRateDraft;
        }

        public ShippingRate GetShippingRate()
        {
            ShippingRate rate = new ShippingRate()
            {
                Price = Money.Parse("1.00 EUR"),
                FreeAbove = Money.Parse("100.00 EUR")
            };
            return rate;
        }

        public ShippingMethod CreateShippingMethod(string shippingCountry = null, string shippingState = null)
        {
            return this.CreateShippingMethod(this.GetShippingMethodDraft(shippingCountry, shippingState));
        }

        public ShippingMethod CreateShippingMethod(ShippingMethodDraft shippingMethodDraft)
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            ShippingMethod shippingMethod = commerceToolsClient
                .ExecuteAsync(new CreateCommand<ShippingMethod>(shippingMethodDraft)).Result;
            return shippingMethod;
        }

        private TaxCategory CreateNewTaxCategory(string country = null, string state = null)
        {
            TaxCategory taxCategory = this.taxCategoryFixture.CreateTaxCategory(country, state);
            this.taxCategoryFixture.TaxCategoriesToDelete.Add(taxCategory);
            return taxCategory;
        }

        private Zone CreateNewZone(string country = null, string state = null)
        {
            Zone zone = this.zonesFixture.CreateZone(country, state);
            this.zonesFixture.ZonesToDelete.Add(zone);
            return zone;
        }
    }
}
