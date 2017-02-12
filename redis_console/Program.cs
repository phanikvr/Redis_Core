using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redis_console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redis = new RedisClient("localhost", 6379))
            {
                string ping = redis.Ping();
                string echo = redis.Echo("hello world");

                //redis.HMSet("mydict", new Dictionary<string, string>
                //    {
                //      { "F1", "string" },
                //      { "F2", "true" },
                //      { "F3", DateTime.Now.ToString() },
                //    });

                Dictionary<string, string> mydict = redis.HGetAll("mydict");

                Console.WriteLine(mydict.Count());

                redis.TransactionQueued += (s, e) =>
                {
                    Console.WriteLine("Transaction queued: {0}({1}) = {2}", e.Command, String.Join(", ", e.Arguments), e.Status);
                };

                redis.Multi();
                var empty1 = redis.Set("test1", "hello"); // returns default(String)
                var empty2 = redis.Set("test2", "world"); // returns default(String)
                var empty3 = redis.Time(); // returns default(DateTime)
                object[] result = redis.Exec();

                foreach (var item in redis.Keys("*"))
                {
                    Console.WriteLine(item);
                }
                
                
            }
        }
    }
}
