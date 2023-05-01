using DG.Tweening;
using UnityEngine;

namespace UI.Components
{
    public class AnimateVibrate : MonoBehaviour
    {
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private float strengthX = 20f;
        [SerializeField] private float strengthY = 0f;

        private Tween _animateTween;

        public void Animate()
        {
            _animateTween?.Kill();
            _animateTween = transform.DOShakePosition(duration, new Vector3(strengthX, strengthY));
        }
    }
}