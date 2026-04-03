using DG.Tweening;
using UnityEngine;

public class Anticlicker : MonoBehaviour
{
    [SerializeField] private PopUpAlphaAnimator _alphaAnimator;

    public void Init() =>
        _alphaAnimator.Init();

    public void Show()
    {
        gameObject.SetActive(true);

        if (_alphaAnimator != null)
            _alphaAnimator.TryPlayShow(out _);
    }

    public void Hide()
    {
        if (_alphaAnimator == null)
            return;

        if (_alphaAnimator.TryPlayHide(out Tweener tweener))
            tweener.OnComplete(() => gameObject.SetActive(false));
    }
}