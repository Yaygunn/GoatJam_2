using UnityEngine;

namespace Yaygun
{
    public static class Extensions 
    {
        public static void Look2D(this Transform transform, Transform target)
        {
            Look2D(transform, target.position);
        }
        
        public static void Look2D(this Transform transform, Vector3 target)
        {
            Vector2 direction = (Vector2)target - (Vector2)transform.position;

            if (direction.sqrMagnitude > 0.0001f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
