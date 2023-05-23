using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class QuizData
    {
        [SerializeField] private string id;
        [SerializeField,Multiline] private string question;
        [SerializeField] private int correctAnswerId;
        [SerializeField] private List<QuizAnswer> answers;
        
        public string ID => id;
        public string Question => question;
        public int CorrectAnswerId => correctAnswerId;


        public List<QuizAnswer> Answers => answers;
        
        public QuizAnswer GetCorrectAnswer()
        {
            return answers[correctAnswerId];
        }
    }

    [Serializable]
    public class QuizAnswer
    {
        [FormerlySerializedAs("id")] [SerializeField] private int index;
        [SerializeField] private string answer;

        public int Index => index;
        public string Answer => answer;

        
    }
}