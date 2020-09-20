using System;
using System.Reflection;

namespace NuclearWar.Infrastructure
{
    public static class Utils
    {
        public static Uri MakePackUri(Type typeInTargetAssembly, string relativeFile)
        {
            Assembly assembly = typeInTargetAssembly.Assembly;
            string assemblyShortName = assembly.ToString().Split(',')[0];
            return new Uri($"pack://application:,,,/{assemblyShortName};component/{relativeFile}"); 
        }
    }
}
