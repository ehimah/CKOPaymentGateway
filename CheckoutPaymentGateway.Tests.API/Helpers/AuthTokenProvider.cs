using System;
using System.IO;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;

namespace CheckoutPaymentGateway.Tests.API.Helpers
{
	public class AuthTokenProvider
	{
        static IConfigurationSection auth0Settings = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build()
             .GetSection("Auth0");

        static string accessToken = string.Empty;

        public static async Task<string> GetAccessToken()
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }

            var auth0Client = new AuthenticationApiClient(auth0Settings["Domain"]);
            var tokenRequest = new ClientCredentialsTokenRequest()
            {
                ClientId = auth0Settings["ClientId"],
                ClientSecret = auth0Settings["ClientSecret"],
                Audience = auth0Settings["Audience"]
            };
            var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

            return tokenResponse.AccessToken;
        }
    }
}

