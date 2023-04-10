using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "ViewPaths", menuName = "ScriptableObjects/ViewPaths")]
    public class ViewPaths:ScriptableObject
    {
        public List<AssetPathForType> ViewAssetsPaths = new List<AssetPathForType>();
    }
}