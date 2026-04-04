using UnityEngine;

namespace FiXiKTestScripts
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private SpriteRenderer _renderer;

        private void OnEnable() =>
            _character.ColorChanged += OnColorChanged;

        private void OnDisable() =>
            _character.ColorChanged -= OnColorChanged;

        private void OnColorChanged(Color color)
        {
            _renderer.color = color;

            Debug.Log("Задан новый цвет:" + color);
        }
    }
}