using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private Character _character;

        public Color Color => _renderer.color;

        private void Awake() =>
            _character = GetComponent<Character>();

        private void OnEnable() =>
            _character.ColorChanged += OnColorChanged;

        private void OnDisable() =>
            _character.ColorChanged -= OnColorChanged;

        private void OnColorChanged(Color color) =>
            _renderer.color = color;
    }
}