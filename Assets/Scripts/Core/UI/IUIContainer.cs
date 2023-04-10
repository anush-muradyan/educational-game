using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface IUIContainer<T>
    {
        Canvas Canvas { get; }
        GraphicRaycaster GraphicRaycaster { get; }
        RectTransform Container { get; }
    }
}