using Pooling;
using UnityEngine;

namespace UI.Components
{
    public class AnswerIcon:MonoBehaviour,IPoolObject
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}