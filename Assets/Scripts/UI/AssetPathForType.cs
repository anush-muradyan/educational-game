using System;
using UnityEngine.AddressableAssets;

namespace UI
{
    [Serializable]
    public class AssetPathForType
    {
        public string AssetType;
        public AssetReference AssetPath;
    }
}