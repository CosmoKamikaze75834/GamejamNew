using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonClickHandler : MonoBehaviour
{
    private Button _button;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnClick);

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClick);
    }

    public abstract void OnClick();
}