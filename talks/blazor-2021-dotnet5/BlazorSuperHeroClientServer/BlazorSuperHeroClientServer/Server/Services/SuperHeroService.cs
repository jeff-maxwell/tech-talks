using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorSuperHeroClientServer.Server.Interfaces;
using BlazorSuperHeroClientServer.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorSuperHeroClientServer.Server.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        IConfiguration _configuration;

        public SuperHeroService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Hero>> Get()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["heros-url"]),
                Headers =
                    {
                        { "x-rapidapi-key", _configuration["x-rapidapi-key"] },
                        { "x-rapidapi-host", _configuration["x-rapidapi-host"] },
                    },
                };

                try
                {
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<List<Hero>>(body);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
    }
}
