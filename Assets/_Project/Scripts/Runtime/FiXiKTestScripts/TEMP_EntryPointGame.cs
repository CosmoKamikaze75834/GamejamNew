using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class TEMP_EntryPointGame : MonoBehaviour
    {
        [SerializeField] private ColorSetConfig _colorSet;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;

        private List<Color> _colors;

        private void Start()
        {
            _colors = new(_colorSet.Colors);

            _player.Init();
            _player.GetComponent<Character>().SetColor(GiveColor());
        }

        private Color GiveColor()
        {
            if (_colors.Count == 0)
                throw new ArgumentNullException("Набор цветов закончился");

            int index = UnityEngine.Random.Range(0, _colors.Count);
            Color color = _colors[index];
            _colors.RemoveAt(index);

            return color;
        }
    }
}