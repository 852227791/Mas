using StackExchange.Redis;
using System;

namespace StackExChangeTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ConfigurationOptions config = new ConfigurationOptions() {
                AllowAdmin = true,
                DefaultDatabase = 0,
                Password = "123456",
                ClientName="10.120.0.110",
                 EndPoints={
                      { "192.168.137.112", 6379 },
                 }
            };
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);
            IDatabase db = redis.GetDatabase(0);
          //  db.StringSet("k1","v1");
            var res = db.StringGet("k1");
            Console.WriteLine(res);
            Console.ReadKey();

        }
    }
}
