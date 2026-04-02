using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEventDispatcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;

    public event Action Clicked;
    public event Action Entered;
    public event Action Exited;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnButtonClick);
    }

    public void OnPointerEnter(PointerEventData eventData) =>
        Entered?.Invoke();

    public void OnPointerExit(PointerEventData eventData) =>
        Exited?.Invoke();

    private void OnButtonClick() =>
        Clicked?.Invoke();
}