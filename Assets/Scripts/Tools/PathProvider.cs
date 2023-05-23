using System.IO;
using UI;
using UnityEngine.AddressableAssets;

namespace Tools
{
    public class PathProvider : IAssetPathProvider
    {
        private readonly ViewPaths viewPaths;

        public PathProvider(ViewPaths viewPaths)
        {
            this.viewPaths = viewPaths;
        }

        public AssetReference ProvidePathForAsset<T>()
        {
            var typeName = typeof(T).Name;
            var pathForType = viewPaths.ViewAssetsPaths.Find(asset => asset.AssetType.Equals(typeName));

            if (pathForType == null)
            {
                throw new DirectoryNotFoundException($"Path For type not found. {typeName}");
            }

            return pathForType.AssetPath;
        }
    }
}