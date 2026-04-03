using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class TweenExtensions
{
    public static Tweener PlayScale(this Transform target, ScaleAnimationConfig config)
    {
        if (target == null || config == null) 
            return null;

        target.DOKill(false);

        Tweener tweener = target.DOScale(config.TargetScale, config.Duration)
            .SetDelay(config.Delay)
            .SetEase(config.Ease)
            .SetLink(target.gameObject);

        return tweener;
    }

    public static Tweener PlayColor(this Image image, ColorAnimationConfig config)
    {
        if (image == null || config == null)
            return null;

        image.DOKill(false);

        Tweener tweener = image.DOColor(config.TargetColor, config.Duration)
            .SetDelay(config.Delay)
            .SetEase(config.Ease)
            .SetLink(image.gameObject);

        return tweener;
    }

    public static Tweener PlayColor(this TMP_Text text, ColorAnimationConfig config)
    {
        if (text == null || config == null)
            return null;

        text.DOKill(false);

        Tweener tweener = text.DOColor(config.TargetColor, config.Duration)
            .SetDelay(config.Delay)
            .SetEase(config.Ease)
            .SetLink(text.gameObject);

        return tweener;
    }
}