using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tools
{
    public class AddressableProvider : IAddressableProvider
    {
        public void InstantiateAsset(AssetReference path, Action<GameObject> callback)
        {
            Debug.Log($"Addressable provider - InstantiateAsset1: {path.RuntimeKey} - {path}");
            Addressables.InstantiateAsync(path).Completed += handle => { callback?.Invoke(handle.Result); };
        }

        public void InstantiateAsset(AssetReference path, Transform parent, Action<GameObject> callback)
        {
            Debug.Log($"Addressable provider - InstantiateAsset2: {path.RuntimeKey} - {path}");
            Addressables.InstantiateAsync(path, parent).Completed += handle => { callback?.Invoke(handle.Result); };
        }

        public void InstantiateAsset(string path, Transform parent, Action<GameObject> callback)
        {
            Debug.Log($"Addressable provider - InstantiateAsset3: {path}");
            Addressables.InstantiateAsync(path, parent).Completed += handle => { callback?.Invoke(handle.Result); };
        }

        public void LoadAsset<T>(object path, Action<T> callback)
        {
            Debug.Log($"Addressable provider - LoadAsset: {path}");
            Addressables.LoadAssetAsync<T>(path).Completed += handle => { callback?.Invoke(handle.Result); };
        }

        public void ReleaseAsset(AssetReference assetReference, GameObject gameObject)
        {
            Debug.Log($"Addressable provider - ReleaseAsset: {assetReference}");
            assetReference.ReleaseInstance(gameObject);
        }
    }
}