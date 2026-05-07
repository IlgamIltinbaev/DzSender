using System;
using System.Collections.Generic;

namespace DzSender.Infrastructures
{
    public class Container
    {
        private Dictionary<Type, object> services;
        public Container()
        {
            services = new Dictionary<Type, object>();
        }
        public void Register<T>(T service)
        {
            services[typeof(T)] = service;
        }
        public T Get<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}
