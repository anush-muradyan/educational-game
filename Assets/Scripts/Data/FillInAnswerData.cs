using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FillInAnswerData
    {
        [SerializeField] private string question;
        [SerializeField] private string id;
        [SerializeField] private float correctAnswer;

        public string Question => question;
        public string ID => id;
        public float CorrectAnswer => correctAnswer;
        
    }
}