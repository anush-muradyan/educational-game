using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.UIContainer
{
    public abstract class AbstractUIContainer<T> : MonoBehaviour, IUIContainer<T>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private RectTransform container;
        public Canvas Canvas => canvas;

        public GraphicRaycaster GraphicRaycaster => graphicRaycaster;

        public RectTransform Container => container;
    }
}