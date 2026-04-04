using System;
using UnityEngine;

namespace FiXiK_Utilites.QuitPanel
{
    [Serializable]
    public struct ButtonColor
    {
        [SerializeField] private Color _image;
        [SerializeField] private Color _text;

        public ButtonColor(Color image, Color text)
        {
            _image = image;
            _text = text;
        }

        public readonly Color Image => _image;

        public readonly Color Text => _text;
    }
}