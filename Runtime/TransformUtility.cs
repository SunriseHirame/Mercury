using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Hirame.Mercury
{
    public static class TransformUtility
    {
        public static Transform CreateGroup (Transform[] others, bool center)
        {
            var parent = new GameObject ("New Group").transform;
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo (parent.gameObject, "Create New Group Parent");
#endif

            if (center)
            {
                var centerPosition = GetCenterPosition (others);
                parent.position = centerPosition;
            }

            foreach (var other in others)
            {
#if UNITY_EDITOR
                Undo.SetTransformParent (other, parent, $"Set parent. {other} {parent}");
#else
                other.SetParent (parent, true);
#endif
            }

            return parent;
        }

        public static Vector3 GetCenterPosition (Transform[] transforms)
        {
            if (transforms == null || transforms.Length == 0)
                return Vector3.zero;

            var center = Vector3.zero;

            foreach (var t in transforms)
            {
                center += t.position;
            }

            return center / transforms.Length;
        }
    }
}