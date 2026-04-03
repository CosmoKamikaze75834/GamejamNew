using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColorAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ColorAnimationConfig _enterConfig;
    [SerializeField] private ColorAnimationConfig _exitConfig;
    [SerializeField] private ColorAnimationConfig _downConfig;
    [SerializeField] private ColorAnimationConfig _upConfig;
    [SerializeField] private ColorAnimationConfig _clickConfig;

    private void Awake()
    {
        if (_exitConfig != null)
            _text.color = _exitConfig.TargetColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            _text.PlayColor(_enterConfig);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitConfig != null)
            _text.PlayColor(_exitConfig);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_downConfig != null)
            _text.PlayColor(_downConfig);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_upConfig != null)
            _text.PlayColor(_upConfig);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickConfig != null)
            _text.PlayColor(_clickConfig);
    }
}