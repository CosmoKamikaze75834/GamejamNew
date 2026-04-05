using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetLayers;

        public List<IEntity> Scan(Vector2 position, float radius)
        {
            List<IEntity> found = new();
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, _targetLayers);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out IEntity entity))
                    found.Add(entity);
            }

            return found;
        }
    }
}