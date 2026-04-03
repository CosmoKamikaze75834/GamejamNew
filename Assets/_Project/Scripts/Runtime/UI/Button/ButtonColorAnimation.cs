using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonColorAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ColorAnimationConfig _enterConfig;
    [SerializeField] private ColorAnimationConfig _exitConfig;

    private Image _image;
    private Color _originalColor;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalColor = _image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            _image.PlayColor(_enterConfig, _enterConfig.TargetColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitConfig != null)
            _image.PlayColor(_exitConfig, _originalColor);
    }
}