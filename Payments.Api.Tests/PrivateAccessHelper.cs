using System;
using System.Reflection;

namespace Payments.Api.Tests
{
    public class PrivateAccessHelper
    {
        public object InvokePrivateMethod<T>(string methodName, T obj, params object[] parameters)
        {
            var method = GetPrivateMethod(methodName, typeof(T));
            return method.Invoke(obj, parameters);
        }

        private MethodInfo GetPrivateMethod(string methodName, Type type)
        {
            return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
