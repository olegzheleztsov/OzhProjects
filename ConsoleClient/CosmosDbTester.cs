using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class CosmosDbTester
    {
        public async Task TestConnectionAsync()
        {
            string connectionString = @"mongodb://olehzheleztsov:mXPE0c5LtvKRC8mf32Oz8YYV2CXXnaHHt4N28fc5X8Q2q0R7pjm81VunzIhg4yZ38afl7ze652huVpQ9pyUGHw==@olehzheleztsov.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@olehzheleztsov@";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var dbNames = await mongoClient.ListDatabaseNamesAsync().ConfigureAwait(false);
            var dbNamesList = await dbNames.ToListAsync().ConfigureAwait(false);
            foreach(var name in dbNamesList)
            {
                Console.WriteLine(name);
            }
        }
    }
}
