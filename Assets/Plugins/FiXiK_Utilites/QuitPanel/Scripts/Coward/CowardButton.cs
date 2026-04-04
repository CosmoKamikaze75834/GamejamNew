using UnityEngine;
using UnityEngine.EventSystems;

namespace FiXiK_Utilites.QuitPanel
{
    public class CowardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _distanceMultiplier = 0.5f;
        [SerializeField] private float _speed = 10f;

        private AcceptButton _core;
        private RectTransform _rectTransform;
        private RectTransform _rectTransformCore;
        private Canvas _canvas;
        private Vector2 _initialPosition;
        private bool _isPointerOverSelf;
        private bool _isPointerOverCore;
        private bool _isCoward;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
            _core = GetComponentInChildren<AcceptButton>(true);
            _rectTransformCore = _core.GetComponent<RectTransform>();
        }

        private void Start() =>
            _initialPosition = _rectTransform.anchoredPosition;

        private void Update()
        {
            bool isOut = _isPointerOverSelf == false && _isPointerOverCore == false;

            if (_isPointerOverSelf && _isPointerOverCore)
                _isCoward = true;
            else if (isOut)
                _isCoward = false;

            if (_isCoward)
                MoveAwayFromCursor();
            else if (isOut)
                MoveBackToInitial();
        }

        private void OnEnable()
        {
            _core.Entered += OnEnterCore;
            _core.Exited += OnExitCore;
        }

        private void OnDisable()
        {
            _core.Entered -= OnEnterCore;
            _core.Exited -= OnExitCore;
        }

        public void OnPointerEnter(PointerEventData eventData) =>
            _isPointerOverSelf = true;

        public void OnPointerExit(PointerEventData eventData) =>
            _isPointerOverSelf = false;

        private void MoveAwayFromCursor()
        {
            Vector2 cursorPos = Utils.GetCursorPositionOnCanvas(_canvas);
            Vector2 direction = cursorPos - _initialPosition;

            if (direction == Vector2.zero)
                return;

            Vector2 awayDir = -direction.normalized;
            float maxDistance = _rectTransformCore.sizeDelta.y * _distanceMultiplier;

            Vector2 target = _initialPosition + awayDir * maxDistance;
            MoveTowards(target);
        }

        private void MoveBackToInitial() =>
            MoveTowards(_initialPosition);

        private void MoveTowards(Vector2 target) =>
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, target, _speed * Time.unscaledDeltaTime);

        private void OnEnterCore() =>
            _isPointerOverCore = true;

        private void OnExitCore() =>
            _isPointerOverCore = false;
    }
}