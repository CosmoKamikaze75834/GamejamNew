using UnityEngine;

namespace FiXiKTestScripts
{
    public class ConspiracyTheory : MonoBehaviour
    {
        [SerializeField] private TextLanguage _text;

        private Transform _target;

        public ConspiracyTheory SetText(LangData data)
        {
            _text.SetData(data);

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