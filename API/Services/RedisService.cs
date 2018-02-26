using System.Threading.Tasks;
using StackExchange.Redis;

namespace services
{
    public class RedisService : IDataService
    {
        public async Task<string> Get(string key)
        {
            using (var redis = await ConnectionMultiplexer.ConnectAsync("redis"))
            {
                var db = redis.GetDatabase();

                var value = await db.StringGetAsync(key);

                return value;
            }
        }
    }
}