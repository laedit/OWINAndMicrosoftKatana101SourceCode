using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SecuredWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string jwt = GetJwtFromTokenIssuer();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var result = client.GetStringAsync("http://localhost:5000/api/employees/123").Result;

            Console.WriteLine(result);
            Console.Read();
        }

        static string GetJwtFromTokenIssuer()
        {
            byte[] key = Convert.FromBase64String("tTW8HB0ebW1qpCmRUEOknEIxaTQ0BFCYrdjOdOI4rfM=");

            var symmetricKey = new InMemorySymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(symmetricKey,
                                                            SecurityAlgorithms.HmacSha256Signature,
                                                            SecurityAlgorithms.Sha256Digest);

            var descriptor = new SecurityTokenDescriptor()
            {
                TokenIssuerName = "http://authzserver.demo",
                AppliesToAddress = "http://localhost:5000/api",
                Lifetime = new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(1)),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new Claim[]
                                             {
                                                 new Claim(ClaimTypes.Name, "Johny")
                                             })
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}