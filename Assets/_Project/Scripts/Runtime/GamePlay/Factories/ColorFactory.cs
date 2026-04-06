using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class ColorFactory
    {
        private readonly List<Color> _colors;

        public ColorFactory(ColorSetConfig colorSetConfig)
        {
            _colors = new(colorSetConfig.Colors);
        }

        public Color Give()
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