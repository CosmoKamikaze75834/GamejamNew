using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Enemy : MonoBehaviour
    {
        private Character _character;

        public void Init()
        {
            _character = GetComponent<Character>();
            _character.Init();
        }
    }
}