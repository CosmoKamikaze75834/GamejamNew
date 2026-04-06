using System.Collections.Generic;
using UnityEngine;

public class AnticlickerMediator : MediatorBase
{
    [SerializeField] private Anticlicker _anticlicker;
    [SerializeField] private List<PopUp> _popUps;

    private int _openedCount;

    private void OnEnable()
    {
        foreach (PopUp popUp in _popUps)
            popUp.Changed += OnPopUpChanged;
    }

    private void OnDisable()
    {
        foreach (PopUp popUp in _popUps)
            popUp.Changed -= OnPopUpChanged;
    }

    public override void Init()
    {
        _anticlicker.Init();

        foreach (PopUp popUp in _popUps)
        {
            if (popUp.gameObject.activeSelf)
                _openedCount++;
        }

        if (_openedCount > 0)
            _anticlicker.Show();
    }

    private void OnPopUpChanged(PopUp popUp)
    {
        if (popUp.gameObject.activeSelf)
        {
            _openedCount++;
            if (_openedCount == 1)
                _anticlicker.Show();
        }
        else
        {
            _openedCount--;
            if (_openedCount == 0)
                _anticlicker.Hide();
        }
    }
}