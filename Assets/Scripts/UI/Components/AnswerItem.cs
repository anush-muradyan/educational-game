using Pooling;
using UnityEngine;

namespace UI.Components
{
    public class AnswerItem:MonoBehaviour,IPoolObject
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