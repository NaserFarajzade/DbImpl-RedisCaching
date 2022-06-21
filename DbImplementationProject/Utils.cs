using System;
using System.Collections.Generic;
using System.Linq;

namespace DbImplementationProject
{
    public class Utils
    {
        public void Query(IEnumerable<string> searchListWithDuplicate)
        {
            var pgdb = new PostgresqlDBHandler();
            var result1 = new List<string>();
            var result2 = new List<string>();
            
            var begin1 = DateTime.Now;
            foreach (var key in searchListWithDuplicate)
            {
                result1.Add(pgdb.GetData(key));
            }
            var end1 = DateTime.Now;
            
            Console.WriteLine($"without redis : {begin1 - end1}");
        
        
            var begin2 = DateTime.Now;
            foreach (var key in searchListWithDuplicate)
            {
                if (RedisDBHandler.HasData(key))
                {
                    var fromRedis = RedisDBHandler.GetData(key);
                    result2.Add(fromRedis);
                    continue;
                }
        
                var fromDB = pgdb.GetData(key);
                result2.Add(fromDB);
                RedisDBHandler.SetData(key, fromDB);
            }
            var end2 = DateTime.Now;
            
            Console.WriteLine($"with redis : {begin2 - end2}");
        }

        public IEnumerable<string> GetRandomSearchListWithDuplicate(
            int fakeFileSize,
            int numberOfQueries, 
            int percentageOfSimilarNumbers,
            int startingRangeOfSimilarNumbers,
            int similarNumbersIntervalLength
        )
        {
            Random random = new Random();
            if (percentageOfSimilarNumbers is > 100 or < 0)
            {
                throw new Exception("invalid range");
            }

            if (startingRangeOfSimilarNumbers + similarNumbersIntervalLength > fakeFileSize)
            {
                throw new Exception("invalid range 2");
            }
    
            var similarNumbersCount = numberOfQueries * percentageOfSimilarNumbers / 100;
            var searchList = new List<int>();
    
            //random numbers between 0 & fakeFileSize
            for (int i = 0; i < numberOfQueries - similarNumbersCount; i++)
            {
                searchList.Add(random.Next(0,fakeFileSize));
            }
            //random numbers between startingRangeOfSimilarNumbers & startingRangeOfSimilarNumbers + similarNumbersIntervalLength
            for (int i = 0; i < similarNumbersCount; i++)
            {
                searchList.Add(random.Next(startingRangeOfSimilarNumbers,startingRangeOfSimilarNumbers+similarNumbersIntervalLength));
            }
    
            return searchList.Select(i => i.ToString());
        }

    }
}