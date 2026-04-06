using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public static class Scanner
    {
        public static List<T> Scan<T>(Vector2 position, float radius, LayerMask targetLayers) where T : class
        {
            List<T> found = new();
            Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, targetLayers);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out T component))
                    found.Add(component);
            }
            return found;
        }

        public static List<IEntity> Scan(Vector2 position, float radius, LayerMask targetLayers)
        {
            return Scan<IEntity>(position, radius, targetLayers);
        }
    }
}