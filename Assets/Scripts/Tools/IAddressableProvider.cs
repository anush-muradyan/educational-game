using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tools
{
    public interface IAddressableProvider
    {
        void InstantiateAsset(AssetReference path, Action<GameObject> callback);
        void InstantiateAsset(AssetReference path, Transform parent, Action<GameObject> callback);
        void InstantiateAsset(string path, Transform parent, Action<GameObject> callback);

        void LoadAsset<T>(object path, Action<T> callback);

        void ReleaseAsset(AssetReference assetReference, GameObject gameObject);
    }
}