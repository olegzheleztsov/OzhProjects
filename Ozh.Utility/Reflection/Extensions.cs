// Create By: Oleg Gelezcov                        (olegg )
// Project: Ozh.Utility     File: Extensions.cs    Created at 2020/09/17/1:43 AM
// All rights reserved, for personal using only
// 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ozh.Utility.Reflection
{
    public static class Extensions
    {
        private static readonly List<Type> _ignorableEnumerables = new List<Type>() {typeof(string)};
        /// <summary>
        /// Copies only public settable properties from source to destination. If property is custom object, then copied only link
        /// If property is IEnumerable, creates list and copy objects to new list. List elements don't copied deeply
        /// </summary>
        /// <param name="destination">Destination object</param>
        /// <param name="source">Source object</param>
        /// <typeparam name="T">Generic object type</typeparam>
        /// Number of affected properties
        public static int CopyPublicProperties<T>(this T destination, T source) where T : class
        {
            var numberOfAffectedProperties = 0;
            if (source == null)
            {
                return numberOfAffectedProperties;
            }
            var publicProperties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            foreach (var property in publicProperties.Where(p => p.CanWrite))
            {
                property.SetValue(destination,
                    IsImplementGenericEnumerable(property.PropertyType)
                        ? CopyGenericCollectionProperty(property, source)
                        : property.GetValue(source));
                numberOfAffectedProperties++;
            }

            return numberOfAffectedProperties;
        }

        private static bool IsImplementGenericEnumerable(Type type)
        {
            if (_ignorableEnumerables.Contains(type))
            {
                return false;
            }
            return type.GetInterfaces().Any(iType =>
                iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        private static object CopyGenericCollectionProperty<T>(PropertyInfo property, T source)
        {
            var genericArgumentType = property.PropertyType.GetGenericArguments().First();
            var listType = typeof(List<>);
            var newListType = listType.MakeGenericType(genericArgumentType);
            var newListInstance = Activator.CreateInstance(newListType);
            var sourceCollection = property.GetValue(source) as IEnumerable;
            if (sourceCollection == null)
            {
                return null;
            }
            var newList = newListInstance as IList;
            if (newList == null) throw new InvalidOperationException(nameof(newList));
            foreach (var obj in sourceCollection) newList.Add(obj);

            return newListInstance;
        }
    }
}