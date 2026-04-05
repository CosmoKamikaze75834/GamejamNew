using TMPro;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class ConspiracyTheory : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Transform _target;

        public ConspiracyTheory SetText(string text)
        {
            _text.text = text;

            return this;
        }

        public ConspiracyTheory SetTarget(Transform target)
        {
            _target = target;

            return this;
        }


        private void Update()
        {
            if (_target != null)
                transform.position = _target.position;
        }
    }
}