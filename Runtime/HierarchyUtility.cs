using UnityEngine;

namespace Hirame.Mercury
{
    public static class HierarchyUtility
    {
        public enum PivotPosition
        {
            Center,
            Top,
            Bottom,
            Left,
            Right,
            Front,
            Back
        }
        

        public static Vector3 GetPivotOffset (MeshFilter[] meshFilters, PivotPosition pivotPosition)
        {
            var center = new Vector3 ();
            var offset = new Vector3 ();

            foreach (var meshFilter in meshFilters)
            {
                var filterTransform = meshFilter.transform;

                var sMesh = meshFilter.sharedMesh;
                var bounds = sMesh.bounds;

                center += filterTransform.InverseTransformPoint (bounds.center);

                if (pivotPosition != PivotPosition.Center)
                    UpdateOffset (ref offset, in bounds, pivotPosition);
            }

            if (pivotPosition == PivotPosition.Center && meshFilters.Length > 0)
                center /= meshFilters.Length;

            return center + offset;
        }
        
        public static void OffsetChildren (Transform transform, in Vector3 offset)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild (i).localPosition += offset;
            }
        }

        private static void UpdateOffset (ref Vector3 offset, in Bounds bounds, PivotPosition pivot)
        {
            var extents = bounds.extents;
            switch (pivot)
            {
                case PivotPosition.Center:
                    break;
                case PivotPosition.Left:
                    if (offset.x < extents.x)
                        offset.x = extents.x;
                    break;
                case PivotPosition.Right:
                    if (offset.x > -extents.x)
                        offset.x = -extents.x;
                    break;
                case PivotPosition.Top:
                    if (offset.y > -extents.y)
                        offset.y = -extents.y;
                    break;
                case PivotPosition.Bottom:
                    if (offset.y < extents.y)
                        offset.y = extents.y;
                    break;
                case PivotPosition.Front:
                    if (offset.z > -extents.z)
                        offset.z = -extents.z;
                    break;
                case PivotPosition.Back:
                    if (offset.z < extents.z)
                        offset.z = extents.z;
                    break;
            }
        }
    }
}