using StackExchange.Redis;

namespace RedisExhange.Web.Api.Services
{
    public interface IRedisService
    {
        public Task ConnectAsync();
        public IDatabase GetDatabase(int db);

    }
}