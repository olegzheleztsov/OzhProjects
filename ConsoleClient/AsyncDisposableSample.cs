using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class AsyncDisposableSample
    {
        public async Task RunAsync()
        {
            
            await using(var obj = new TestAsyncDisposable())
            {
                Console.WriteLine("inside using..");
            }
            Console.WriteLine("after using");
            Console.ReadLine();
        }
    }

    public class TestAsyncDisposable : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("dispose async...");
            await new ValueTask();
        }
    }
}
