#if UNITY_EDITOR

#endif
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class EditorTools
    {
        [MenuItem("Educational Game/Storage/Clear Editor Prefs Storage")]
        public static void ClearFlagGameData()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "FlagGameData.json")))
            {
                return;
            }

            File.Delete(Path.Combine(Application.persistentDataPath, "FlagGameData.json"));
            Debug.Log("Clear FLag Data");
        }
    }
}
