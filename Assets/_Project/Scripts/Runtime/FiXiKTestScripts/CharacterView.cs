using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Color _color;

        private void OnEnable()
        {
            _character.ColorChanged += OnColorChanged;
            OnColorChanged(_character.Color);
        }

        private void OnDisable() =>
            _character.ColorChanged += OnColorChanged;

        private void OnColorChanged(Color color)
        {
            _color = color;

            Debug.Log("Задан новый цвет:" + color);
        }
    }
}