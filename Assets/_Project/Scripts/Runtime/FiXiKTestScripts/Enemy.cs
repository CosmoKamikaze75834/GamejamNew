using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Enemy : MonoBehaviour, IAttacker
    {
        private Character _character;

        public Color Color => throw new System.NotImplementedException();

        public Transform Transform => throw new System.NotImplementedException();

        public void Init()
        {
            _character = GetComponent<Character>();
            _character.Init();
        }
    }
}