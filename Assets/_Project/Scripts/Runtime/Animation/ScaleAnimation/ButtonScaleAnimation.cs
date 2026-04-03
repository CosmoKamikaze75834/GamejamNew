using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler
{
    [SerializeField] private ScaleAnimationConfig _enterConfig;
    [SerializeField] private ScaleAnimationConfig _exitConfig;
    [SerializeField] private ScaleAnimationConfig _downConfig;
    [SerializeField] private ScaleAnimationConfig _upConfig;
    [SerializeField] private ScaleAnimationConfig _clickConfig;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;

        if (_exitConfig != null)
            _transform.localScale = _exitConfig.TargetScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            _transform.PlayScale(_enterConfig);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitConfig != null)
            _transform.PlayScale(_exitConfig);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_downConfig != null)
            _transform.PlayScale(_downConfig);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_upConfig != null)
            _transform.PlayScale(_upConfig);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickConfig != null)
            _transform.PlayScale(_clickConfig);
    }
}
