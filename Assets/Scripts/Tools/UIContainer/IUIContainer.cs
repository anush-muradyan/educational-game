using UnityEngine;
using UnityEngine.UI;

namespace Tools.UIContainer
{
    public interface IUIContainer<T>
    {
        Canvas Canvas { get; }
        GraphicRaycaster GraphicRaycaster { get; }
        RectTransform Container { get; }
    }
}