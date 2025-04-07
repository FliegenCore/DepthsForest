using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public static class G 
    {
        private static List<IService> _services = new List<IService>();
        private static Dictionary<Type, IService> _servicesByType = new Dictionary<Type, IService>();

        public static IReadOnlyList<IService> Services => _services;

        public static void CreateService<T>() where T : Component, IService
        {
            if (_servicesByType.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"Service {typeof(T)} already exists!");
                return;
            }
            
            GameObject gameObject = new GameObject(typeof(T).Name);
            T service = gameObject.AddComponent<T>();
            
            Register(service);
        }

        public static void InitializeServices()
        {
            foreach (var service in _services)
            {
                service.Initialize();
            }
        }
        
        public static T Get<T>() where T : IService
        {
            if (_servicesByType.TryGetValue(typeof(T), out IService service))
            {
                return (T)service;
            }
            
            Debug.LogWarning($"Service {typeof(T)} not found!");
            return default;
        }
        
        public static void Register<T>(T service) where T : IService
        {
            Type type = typeof(T);
            
            if (_servicesByType.ContainsKey(type))
            {
                Debug.LogWarning($"Service {type} already registered!");
                return;
            }
            
            _services.Add(service);
            _servicesByType.Add(type, service);
        }

        public static void Unregister<T>() where T : IService
        {
            Type type = typeof(T);
            
            if (!_servicesByType.TryGetValue(type, out IService service))
            {
                Debug.LogWarning($"Service {type} not registered!");
                return;
            }
            
            _services.Remove(service);
            _servicesByType.Remove(type);
        }
        
        public static void UnregisterAllDestroyed()
        {
            for (int i = _services.Count - 1; i >= 0; i--)
            {
                IService service = _services[i];
                
                if (service == null || service.Equals(null))
                {
                    _services.RemoveAt(i);
                    _servicesByType.Remove(service.GetType());
                    continue;
                }

                if (service.CanDestroyed)
                {
                    if (service is MonoBehaviour monoBehaviour)
                    {
                        GameObject.Destroy(monoBehaviour.gameObject);
                    }
                    
                    _services.RemoveAt(i);
                    _servicesByType.Remove(service.GetType());
                }
            }
        }
    }
}