using UnityEngine;

namespace TT
{
    public static class RaycastUtil
    {
        public static Collider TryGetPickupableCollider(Camera camera, float range, LayerMask layerMask)
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, layerMask))
            {
                return hit.collider;
            }

            return null;
        }
    }   
}