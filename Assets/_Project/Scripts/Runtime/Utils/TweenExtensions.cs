using DG.Tweening;
using UnityEngine;

public static class TweenExtensions
{
    public static Tweener PlayScale(this Transform target, ScaleAnimationConfig config, Vector3 endScale)
    {
        if (target == null || config == null) 
            return null;

        target.DOKill(false);

        Tweener tweener = target.DOScale(endScale, config.Duration)
            .SetDelay(config.Delay)
            .SetEase(config.Ease)
            .SetLink(target.gameObject);

        return tweener;
    }
}