using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Radio : MonoBehaviour
    {
        public event Action<Radio, IEntity> Entered;

        public void Init() =>
            GetComponent<Collider2D>().isTrigger = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IEntity entity))
                Entered?.Invoke(this, entity);
        }

        public void Destroy() =>
            Destroy(gameObject);
    }
}