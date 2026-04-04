using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiXiK_Utilites.QuitPanel
{
    [Serializable]
    public class MenuButtonView
    {
        [SerializeField] private ButtonColor _origin;
        [SerializeField] private ButtonColor _enter;
        [SerializeField] private ButtonColor _down;
        [SerializeField] private float _repaintTime = 0.2f;
        [SerializeField] private bool _isRepaint = true;

        private Image _image;
        private TextMeshProUGUI _text;
        private Coroutine _coroutine;

        private ButtonColor _targetColor;
        private ButtonColor _startColor;
        private float _progress;

        public void Init(MenuButton menuButton)
        {
            _image = menuButton.GetComponentInChildren<Image>(true);
            _text = menuButton.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void SetOrigin() =>
            SetButtonColor(_origin);

        public void SetEnter() =>
            SetButtonColor(_enter);

        public void SetDown() =>
            SetButtonColor(_down);

        private void SetButtonColor(ButtonColor buttonColor)
        {
            if (_isRepaint == false)
                return;

            _targetColor = buttonColor;
            _startColor = new(_image.color, _text.color);
            _progress = 0f;

            if (_coroutine != null)
                _image.StopCoroutine(_coroutine);

            _coroutine = _image.StartCoroutine(RepaintRoutine());
        }

        private IEnumerator RepaintRoutine()
        {
            while (_progress < 1f)
            {
                _progress += Time.unscaledDeltaTime / _repaintTime;
                _progress = Mathf.Clamp01(_progress);

                float smoothInterval = EaseInOutQuad(_progress);

                _image.color = Color.Lerp(
                    _startColor.Image,
                    _targetColor.Image,
                    smoothInterval
                );

                _text.color = Color.Lerp(
                    _startColor.Text,
                    _targetColor.Text,
                    smoothInterval
                );

                yield return null;
            }
        }

        private float EaseInOutQuad(float x)
        {
            return x < 0.5f
                ? 2f * x * x
                : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
        }
    }
}