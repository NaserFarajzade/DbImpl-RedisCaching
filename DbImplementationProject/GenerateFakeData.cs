using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace DbImplementationProject
{
    public class GenerateFakeData
    {
        private static Random random = new();

        private static List<string> names = new()
            {"naser", "ali", "mohammad", "maryam", "zahra", "hossein", "sara", "reza", "soroosh", "fateme"};
        
        public static async Task GenerateToDatabase(int fakeFileSize,PostgresqlDBHandler dbHandler)
        {
            for (int i = 0; i < fakeFileSize; i++)
            {
                dbHandler.SetData(i.ToString(), names[random.Next(0, names.Count)]);
            }
        }
    }
}

