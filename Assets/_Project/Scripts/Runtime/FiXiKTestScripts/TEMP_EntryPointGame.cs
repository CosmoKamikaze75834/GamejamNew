using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class TEMP_EntryPointGame : MonoBehaviour
    {
        [SerializeField] private ColorSetConfig _colorSet;
        [SerializeField] private Player _player;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private ConspiracyTheory _conspiracyTheoryPrefab;
        [SerializeField] private List<string> _conspiracyTheories;

        private List<Color> _colors;

        private void Awake()
        {
            _colors = new(_colorSet.Colors);

            Shooter shooter = new(_player, _bulletPrefab);
            _player.SetShooter(shooter);
            _player.GetComponent<Character>().SetColor(GiveColor());

            ConspiracyTheory conspiracyTheory = Instantiate(_conspiracyTheoryPrefab, _player.transform.position, Quaternion.identity);
            conspiracyTheory.SetText(GiveTheory()).SetTarget(_player.transform);
        }

        public Color GiveColor()
        {
            if (_colors.Count == 0)
                throw new ArgumentNullException("Набор цветов закончился");

            int index = UnityEngine.Random.Range(0, _colors.Count);
            Color color = _colors[index];
            _colors.RemoveAt(index);

            return color;
        }

        public string GiveTheory()
        {
            if (_conspiracyTheories.Count == 0)
                throw new ArgumentNullException("Набор цветов закончился");

            int index = UnityEngine.Random.Range(0, _conspiracyTheories.Count);
            string text = _conspiracyTheories[index];
            _conspiracyTheories.RemoveAt(index);

            return text;
        }
    }
}