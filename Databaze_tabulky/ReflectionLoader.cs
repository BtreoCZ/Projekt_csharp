using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Databaze_tabulky
{
    public class ReflectionLoader
    {
        private readonly string assemblyPath;

        public ReflectionLoader(string assemblyPath)
        {
            this.assemblyPath = assemblyPath;
        }

        public object? CreateInstance(string typeName, object[] constructorArgs)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            Type? type = assembly.GetType(typeName);

            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type, constructorArgs);
        }

        public PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties();
        }

        public Type? GetTypeOf(string typeName)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            return assembly.GetType(typeName);
        }
    }
}
