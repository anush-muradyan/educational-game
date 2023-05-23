#if UNITY_EDITOR

#endif
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class EditorTools
    {
        [MenuItem("Educational Game/Storage/Clear Flag Game Storage")]
        public static void ClearFlagGameData()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "FlagGameData.json")))
            {
                return;
            }

            File.Delete(Path.Combine(Application.persistentDataPath, "FlagGameData.json"));
            Debug.Log("Clear FLag Data");
        } 
        
        [MenuItem("Educational Game/Storage/Clear Capitals Game Storage")]
        public static void ClearCapitalsGameData()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "CountryGameData.json")))
            {
                return;
            }

            File.Delete(Path.Combine(Application.persistentDataPath, "CountryGameData.json"));
            Debug.Log("Clear Capitals Data");
        }
        
        [MenuItem("Educational Game/Storage/Clear Progression Game Storage")]
        public static void ClearProgressionGameData()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "ProgressionGameData.json")))
            {
                return;
            }

            File.Delete(Path.Combine(Application.persistentDataPath, "ProgressionGameData.json"));
            Debug.Log("Clear Progression Game Data");
        }
        
        [MenuItem("Educational Game/Storage/Clear Expressions Game Storage")]
        public static void ClearExpressionsGameData()
        {
            var path = Path.Combine(Application.persistentDataPath, "ExpressionsGameData.json");
            if (!File.Exists(path))
            {
                return;
            }

            File.Delete(path);
            Debug.Log("Clear Expressions Game Game Data");
        }
        
        [MenuItem("Educational Game/Storage/Clear Planimetrics Game Storage")]
        public static void ClearPlanimetricsGameData()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "PlanimetricsGameData.json")))
            {
                return;
            }

            File.Delete(Path.Combine(Application.persistentDataPath, "PlanimetricsGameData.json"));
            Debug.Log("Clear Planimetrics Game Game Data");
        }
    }
}
