using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private List<PopUp> _popUpsToHide;
    [SerializeField] private PopUpScaleAnimator _scaleAnimator;
    [SerializeField] private PopUpAlphaAnimator _alphaAnimator;
    [SerializeField] private PopUpAudio _audio;

    public event Action<PopUp> Changed;

    public bool IsActive { get; private set; }

    public void Init()
    {
        _scaleAnimator.Init();
        _alphaAnimator.Init();
        IsActive = gameObject.activeSelf;
    }

    public void Show()
    {
        if (IsActive)
            return;

        IsActive = true;
        gameObject.SetActive(true);

        if (_scaleAnimator != null)
            _scaleAnimator.TryPlayShow(out _);

        if (_alphaAnimator != null)
            _alphaAnimator.TryPlayShow(out _);

        if (_audio != null)
            _audio.PlayShowing();

        Changed?.Invoke(this);

        foreach (PopUp popUp in _popUpsToHide)
            popUp.Hide();
    }

    public void Hide()
    {
        Debug.Log(nameof(Hide));

        if (IsActive == false)
            return;

        IsActive = false;

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

        Sequence sequence = DOTween.Sequence()
            .SetUpdate(true);

        foreach (Tweener tweener in tweeners)
            sequence.Join(tweener);

        if (_audio != null)
            _audio.PlayHidding();

        sequence.OnComplete(() =>
        {
            HideInternal();
        });
    }

    public void FastHide()
    {
        gameObject.SetActive(false);
        IsActive = false;
    }

    private void HideInternal()
    {
        gameObject.SetActive(false);
        Changed?.Invoke(this);
    }
}