// Create By: Oleg Gelezcov                        (olegg )
// Project: ConsoleClient     File: ReflectionCases.cs    Created at 2020/09/17/1:52 AM
// All rights reserved, for personal using only
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleClient.ReflectionTests
{
    public class ReflectionCases
    {
        public void CheckListType()
        {
            var lst = new List<string>() {"one"};
            Console.WriteLine(lst.GetType().IsAssignableFrom(typeof(List<string>)));
        }

        public void CheckRegardlessOfTheGenericType()
        {
            var lst = new List<string>() {"one"};
            Console.WriteLine(lst.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)));
            var genericArgument = lst.GetType().GetGenericArguments().First();
            Console.WriteLine(genericArgument.Name);
        }
    }
}