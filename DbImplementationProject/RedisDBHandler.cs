using StackExchange.Redis;

namespace DbImplementationProject
{
    public static class RedisDBHandler
    {
        private static readonly ConnectionMultiplexer connection = RedisConnectorHelper.Connection;

        public static bool SetData(string key, string value)
        {
            var cache = connection.GetDatabase();
            var res = cache.StringSet(key, value);
            return res;
        }
        
        public static string GetData(string key)
        {
            var cache = connection.GetDatabase();
            var res = cache.StringGet(key);
            return res;
        }
        
        public static bool HasData(string key)
        {
            var cache = connection.GetDatabase();
            var res = cache.KeyExists(key);
            return res;
        }
    }
}