using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    public static Tweener PlayColor(this Image image, ColorAnimationConfig config, Color endColor)
    {
        if (image == null || config == null)
            return null;

        image.DOKill(false);

        Tweener tweener = image.DOColor(endColor, config.Duration)
            .SetDelay(config.Delay)
            .SetEase(config.Ease)
            .SetLink(image.gameObject);

        return tweener;
    }
}