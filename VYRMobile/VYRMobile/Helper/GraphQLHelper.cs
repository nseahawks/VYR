using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Helper
{
    public class GraphQLHelper
    {
        public readonly GraphQLHttpClient graphQLClient;

        public GraphQLHelper()
        {
            graphQLClient = new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(Constants.AppSyncEndpoint)

            }, new NewtonsoftJsonSerializer());

            graphQLClient.HttpClient.DefaultRequestHeaders.Add("x-api-key", Constants.AppSyncApiKey);
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("host", Constants.AppSyncHost);
        }
    }
}
