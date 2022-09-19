using System;
using StackExchange.Redis;

namespace RedisExhange.Web.Api.Services
{
    public class RedisService: IRedisService
    {
        private readonly string? _redisHost;
        private readonly string? _redisPort;
        private IDatabase _db { get; set; }
        private ConnectionMultiplexer _redis;

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }
        public async Task ConnectAsync()
        {
            var configString = $"{_redisHost}:{_redisPort}";
            _redis =  await ConnectionMultiplexer.ConnectAsync(configString);
        }

        public IDatabase GetDatabase(int db)
        {
            return  _redis.GetDatabase(db);
        }

       
    }
}

