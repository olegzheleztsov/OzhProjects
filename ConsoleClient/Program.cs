using System.Threading.Tasks;
using ConsoleClient.ReflectionTests;

namespace ConsoleClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var reflectionTest = new ReflectionCases();
            reflectionTest.CheckListType();
            reflectionTest.CheckRegardlessOfTheGenericType();
        }

        private static async Task RabbitMQSample()
        {
            //SignalRTestClient client = new SignalRTestClient();
            //await client.Run().ConfigureAwait(false);
            //AsyncDisposableSample sample = new AsyncDisposableSample();
            //await sample.RunAsync().ConfigureAwait(false);

            //var cosmosTester = new CosmosDbTester();
            //await cosmosTester.TestConnectionAsync().ConfigureAwait(false);
            //CheckDateTimeInJsonConversion();

            RabbitMQTest.RabbitMQProg.Run();
            await Task.CompletedTask.ConfigureAwait(false);           
        }

        private static void CheckDateTimeInJsonConversion()
        {
            var tester = new JsonConversionTester();
            tester.HowIsDateTimeConverted();
        }
    }
}
