using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class ConspiracyTheoryFactory
    {
        private readonly ConspiracyTheory _prefab;
        private readonly List<LangData> _theories;

        public ConspiracyTheoryFactory(ConspiracyTheory prefab, ConspiracyTheoryConfig config)
        {
            _prefab = prefab;
            _theories = new(config.Theories);
        }

        public ConspiracyTheory Get(Transform target)
        {
            if (_theories.Count == 0)
                throw new ArgumentNullException("Набор теорий закончился");

            int index = UnityEngine.Random.Range(0, _theories.Count);
            ConspiracyTheory theory = UnityEngine.Object.Instantiate(_prefab);
            theory.SetText(_theories[index]).SetTarget(target);
            _theories.RemoveAt(index);

            return theory;
        }
    }
}