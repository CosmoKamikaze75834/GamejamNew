using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ScaleAnimationConfig _enterConfig;
    [SerializeField] private ScaleAnimationConfig _exitConfig;

    private Vector3 _originalScale;

    private void Awake() =>
        _originalScale = transform.localScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enterConfig != null)
            transform.PlayScale(_enterConfig, _enterConfig.TargetScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_exitConfig != null)
            transform.PlayScale(_exitConfig, _originalScale);
    }
}