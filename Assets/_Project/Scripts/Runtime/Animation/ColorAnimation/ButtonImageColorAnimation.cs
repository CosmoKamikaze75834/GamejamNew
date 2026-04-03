using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonImageColorAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler
{
    [SerializeField] private ColorAnimationConfig _enterConfig;
    [SerializeField] private ColorAnimationConfig _exitConfig;
    [SerializeField] private ColorAnimationConfig _downConfig;
    [SerializeField] private ColorAnimationConfig _upConfig;
    [SerializeField] private ColorAnimationConfig _clickConfig;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();

        if (_exitConfig != null)
            _image.color = _exitConfig.TargetColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            _image.PlayColor(_enterConfig);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitConfig != null)
            _image.PlayColor(_exitConfig);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_downConfig != null)
            _image.PlayColor(_downConfig);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_upConfig != null)
            _image.PlayColor(_upConfig);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickConfig != null)
            _image.PlayColor(_clickConfig);
    }
}