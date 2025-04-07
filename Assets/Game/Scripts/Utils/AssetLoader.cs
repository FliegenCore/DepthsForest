using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public static class AssetLoader 
    {
        private static Dictionary<string, object> _loadedAssets = new Dictionary<string, object>();

        public static T LoadSync<T>(string key) where T : UnityEngine.Object
        {
            if (_loadedAssets.ContainsKey(key))
            {
                return (T)_loadedAssets[key];
            }

            T asset = Resources.Load<T>(key);

            if (asset != null)
            {
                _loadedAssets.Add(key, asset);
            }
            else
            {
                Debug.LogWarning("Asset loading failed: " + key);
            }

            return asset;
        }

        public static T InstantiateSync<T>(T handle, Transform parent) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }
    }
}