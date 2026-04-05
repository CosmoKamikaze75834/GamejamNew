using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public static class Scanner
    {
        public static List<IEntity> Scan(Vector2 position, float radius, LayerMask targetLayers)
        {
            List<IEntity> found = new();
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, targetLayers);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out IEntity entity))
                    found.Add(entity);
            }

            return found;
        }
    }
}