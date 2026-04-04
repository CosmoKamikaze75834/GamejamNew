using UnityEngine;
using UnityEngine.EventSystems;

namespace FiXiK_Utilites.QuitPanel
{
    public class BraveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _distanceMultiplier = 0.49f;
        [SerializeField] private float _speed = 10f;

        private RectTransform _rectTransform;
        private RectTransform _rectTransformCore;
        private Canvas _canvas;
        private Vector2 _initialPosition;
        private bool _isPointerOverSelf;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
            CancelButton core = GetComponentInChildren<CancelButton>(true);
            _rectTransformCore = core.GetComponent<RectTransform>();
        }

        private void Start() =>
            _initialPosition = _rectTransform.anchoredPosition;

        private void Update()
        {
            if (_isPointerOverSelf)
                MoveToCursor();
            else
                MoveBackToInitial();
        }

        public void OnPointerEnter(PointerEventData eventData) =>
            _isPointerOverSelf = true;

        public void OnPointerExit(PointerEventData eventData) =>
            _isPointerOverSelf = false;

        private void MoveToCursor()
        {
            Vector2 cursorPos = Utils.GetCursorPositionOnCanvas(_canvas);
            Vector2 direction = cursorPos - _initialPosition;

            if (direction == Vector2.zero)
                return;

            direction = direction.normalized;
            float maxDistance = _rectTransformCore.sizeDelta.y * _distanceMultiplier;
            Vector2 target = _initialPosition + direction * maxDistance;

            MoveTowards(target);
        }

        private void MoveBackToInitial() =>
            MoveTowards(_initialPosition);

        private void MoveTowards(Vector2 target) =>
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, target, _speed * Time.unscaledDeltaTime);
    }
}