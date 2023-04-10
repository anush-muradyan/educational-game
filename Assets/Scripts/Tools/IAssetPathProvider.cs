using UnityEngine.AddressableAssets;

namespace Tools
{
    public interface IAssetPathProvider {
        public AssetReference ProvidePathForAsset<T>();
    }
}