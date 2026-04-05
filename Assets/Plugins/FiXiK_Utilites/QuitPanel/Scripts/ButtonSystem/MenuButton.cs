using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FiXiK_Utilites.QuitPanel
{
    public class MenuButton : MonoBehaviour, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerDownHandler, 
        IPointerClickHandler
    {
        [SerializeField] private MenuButtonView _buttonView;
        [SerializeField] private ButtonAnimator _animator;
        [SerializeField] private bool _isAnimate = true;

        public event Action Clicked;
        public event Action Entered;
        public event Action Exited;

        private void Awake() =>
            _buttonView.Init(this);

        private void OnEnable() =>
            _buttonView.SetOrigin();

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonView.SetEnter();

            if (_isAnimate)
                _animator.SetEnter();

            Entered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonView.SetOrigin();

            if (_isAnimate)
                _animator.SetExit();

            Exited?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData) =>
            _buttonView.SetDown();

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _buttonView.SetEnter();

            Clicked?.Invoke();
        }
    }
}