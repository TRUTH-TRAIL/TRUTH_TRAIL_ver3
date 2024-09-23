using UnityEngine;

namespace TT
{
    public enum SlideDirection
    {
        Left,
        Right,
        Forward,
        Backward
    }
    
    public static class MovementUtils
    {
        public static Vector3 GetSlideVector(Transform transform, SlideDirection direction)
        {
            switch (direction)
            {
                case SlideDirection.Left:
                    return -transform.right; // 로컬 좌표에서의 왼쪽
                case SlideDirection.Right:
                    return transform.right; // 로컬 좌표에서의 오른쪽
                case SlideDirection.Forward:
                    return transform.forward; // 로컬 좌표에서의 앞으로
                case SlideDirection.Backward:
                    return -transform.forward; // 로컬 좌표에서의 뒤로
                default:
                    return transform.right; // 기본값은 로컬 좌표에서의 오른쪽
            }
        }
    }
}