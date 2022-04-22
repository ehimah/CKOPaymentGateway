using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace CheckoutPaymentGateway.Tests.API.Helpers
{
	public static class FakeTokenProvider
	{

        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static string Audience { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private static readonly RandomNumberGenerator generator = RandomNumberGenerator.Create();
        private static readonly byte[] key = new byte[32];

        static FakeTokenProvider()
        {
            // generate and populate key with random bytes
            generator.GetBytes(key);

            // build security key from random key
            SecurityKey = new SymmetricSecurityKey(key) { KeyId = Guid.NewGuid().ToString() };

            // generate signin credentials from security
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public static string GetAccessToken()
        {
            return tokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, null, null, DateTime.UtcNow.AddMinutes(10), SigningCredentials));
        }
    }
}

