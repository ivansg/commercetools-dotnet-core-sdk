using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Messages;
using commercetools.Sdk.Domain.Query;
using Xunit;

namespace commercetools.Sdk.HttpApi.IntegrationTests.Messages
{
    [Collection("Integration Tests")]
    public class MessagesIntegrationTests : IClassFixture<MessagesFixture>
    {
        private readonly MessagesFixture messagesFixture;

        public MessagesIntegrationTests(MessagesFixture messagesFixture)
        {
            this.messagesFixture = messagesFixture;
        }
        
        [Fact]
        public void QueryAndSortMessagesDescending()
        {
            IClient commerceToolsClient = this.messagesFixture.GetService<IClient>();
            
            QueryPredicate<Message> queryPredicate = new QueryPredicate<Message>(m => m.Type == "CustomerEmailVerified" || m.Type=="CategoryCreated");
            
            //QueryPredicate<Message> queryPredicate = new QueryPredicate<Message>(m => m.Version >=1);
            
            List<Sort<Message>> sortPredicates = new List<Sort<Message>>();
            Sort<Message> sort = new Sort<Message>(m=>m.CreatedAt, SortDirection.Descending);
            sortPredicates.Add(sort);
            
            QueryCommand<Message> queryCommand = new QueryCommand<Message>();
            queryCommand.SetWhere(queryPredicate);
            queryCommand.SetSort(sortPredicates);
            //queryCommand.Limit = 100;
            
            PagedQueryResult<Message> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;
            Assert.True(returnedSet.Count > 0);
        }
    }
}