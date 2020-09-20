// Create By: Oleg Gelezcov                        (olegg )
// Project: ConsoleClient     File: JsonConversionTester.cs    Created at 2020/09/02/7:56 AM
// All rights reserved, for personal using only
// 

using System;
using System.Text.Json;

namespace ConsoleClient
{
    public class JsonConversionTester
    {
        public void HowIsDateTimeConverted()
        {
            var blog = new Blog()
            {
                Title = "its title",
                Content = "it is content",
                Time = DateTime.UtcNow
            };
            var json = JsonSerializer.Serialize(blog, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            Console.WriteLine(json);
        }
    }
}