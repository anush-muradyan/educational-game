using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName ="FlagsQuizData",menuName = "ScriptableObjects/FlagsQuizData")]
    public class FlagsQuizData:ScriptableObject
    {
        [SerializeField] private List<FlagData> flagData;
        public List<FlagData> FlagData => flagData;
    }
}