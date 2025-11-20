using UnityEngine;
using DG.Tweening;

namespace MiniIT.ARKANOID
{
    public class GameHudAnimator
    {
        private const float OVERSHOOT_SCALE = 1.1f;
        private const float GROW_DURATION = 0.2f;
        private const float SETTLE_DURATION = 0.1f;
        private const float TILT_ANGLE = 8.0f;
        private const float TILT_DURATION = 0.1f;
        
        private Tween       _winWindowTween;
        private Tween       _loseWindowTween;
    
        public void PlayWinWindow(RectTransform target)
        {
            if (target == null)
            {
                return;
            }
    
            _winWindowTween?.Kill();
            _winWindowTween = PlayWindowShowAnimation(target);
        }
    
        public void PlayLoseWindow(RectTransform target)
        {
            if (target == null)
            {
                return;
            }
    
            _loseWindowTween?.Kill();
            _loseWindowTween = PlayWindowShowAnimation(target);
        }
    
        public void KillAllTweens()
        {
            _winWindowTween?.Kill();
            _loseWindowTween?.Kill();
    
            _winWindowTween = null;
            _loseWindowTween = null;
        }
    
        private Tween PlayWindowShowAnimation(RectTransform target)
        {
            if (target == null)
            {
                return null;
            }
    
            target.localScale = Vector3.zero;
            target.localRotation = Quaternion.identity;
    
            Sequence sequence = DOTween.Sequence();
    
            sequence.Append(target.DOScale(OVERSHOOT_SCALE, GROW_DURATION).SetEase(Ease.OutBack));
            sequence.Join(target.DOLocalRotate(new Vector3(0.0f, 0.0f, TILT_ANGLE), TILT_DURATION).SetLoops(2, LoopType.Yoyo));
            sequence.Append(target.DOScale(1.0f, SETTLE_DURATION).SetEase(Ease.OutQuad));
            sequence.OnComplete(() =>
            {
                target.localScale = Vector3.one;
                target.localRotation = Quaternion.identity;
            });
    
            return sequence;
        }
    }
}