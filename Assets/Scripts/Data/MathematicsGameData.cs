using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MathematicsGameData", menuName = "ScriptableObjects/MathematicsGameData")]

    public class MathematicsGameData : ScriptableObject
    {
        [SerializeField] private List<FillInAnswerData> progressionQuizData;
        [SerializeField] private List<QuizData> expressionsQuizData;
        [SerializeField] private List<QuizData> planimetricsQuizData;

        public List<FillInAnswerData> ProgressionQuizData => progressionQuizData;
        public List<QuizData> ExpressionsQuizData => expressionsQuizData;
        public List<QuizData> PlanimetricsQuizData => planimetricsQuizData;
    }
}