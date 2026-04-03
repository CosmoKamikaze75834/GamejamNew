using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonShakeAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler
{
    [SerializeField] private ShakeAnimationConfig _enterConfig;

    private Transform _transform;
    Vector3 _originalPosition;
    Quaternion _originalRotation;

    private void Awake() =>
        _transform = transform;

    private void Start()
    {
        _originalPosition = _transform.localPosition;
        _originalRotation = _transform.localRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            _transform.PlayShake(_enterConfig);
    }

    public void OnPointerExit(PointerEventData eventData) =>
        StopShake();

    public void OnPointerDown(PointerEventData eventData) =>
        StopShake();

    public void OnPointerUp(PointerEventData eventData) =>
        StopShake();

    public void OnPointerClick(PointerEventData eventData) =>
        StopShake();

    private void StopShake()
    {
        _transform.StopShake();
        _transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);
    }
}