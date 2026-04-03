using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private PopUpScaleAnimator _scaleAnimator;
    [SerializeField] private PopUpAlphaAnimator _alphaAnimator;
    [SerializeField] private PopUpAudio _audio;

    public event Action<PopUp> Changed;

    public void Init()
    {
        _scaleAnimator.Init();
        _alphaAnimator.Init();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (_scaleAnimator != null)
            _scaleAnimator.TryPlayShow(out _);

        if (_alphaAnimator != null)
            _alphaAnimator.TryPlayShow(out _);

        if (_audio != null)
            _audio.PlayShowing();

        Changed?.Invoke(this);
    }

    public void Hide()
    {
        List<Tweener> tweeners = new();

        if (_scaleAnimator != null)
        {
            if (_scaleAnimator.TryPlayHide(out Tweener tweener))
                tweeners.Add(tweener);
        }

        if (_alphaAnimator != null)
        {
            if (_alphaAnimator.TryPlayHide(out Tweener tweener))
                tweeners.Add(tweener);
        }

        if (tweeners.Count == 0)
        {
            gameObject.SetActive(false);

            return;
        }

        Sequence sequence = DOTween.Sequence();

        foreach (Tweener tweener in tweeners)
            sequence.Join(tweener);

        if (_audio != null)
            _audio.PlayHidding();

        sequence.OnComplete(() => HideInternal());
    }

    private void HideInternal()
    {
        gameObject.SetActive(false);
        Changed?.Invoke(this);
    }
}