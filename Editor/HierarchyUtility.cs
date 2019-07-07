using UnityEditor;
using UnityEngine;

namespace Hirame.Mercury
{
    public static class HierarchyUtility
    {
        private enum PivotPosition
        {
            Center,
            Top,
            Bottom,
            Left,
            Right,
            Front,
            Back
        }

        private const int PivotMenuPriority = -10;
        
        
        [MenuItem ("GameObject/Pivot/Center", false, PivotMenuPriority)]
        private static void SetPivotToCenter (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Center);
        }
        [MenuItem ("GameObject/Pivot/Bottom", false, PivotMenuPriority)]
        private static void SetPivotToBottom (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Bottom);
        }
        
        [MenuItem ("GameObject/Pivot/Top", false, PivotMenuPriority)]
        private static void SetPivotToTop (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Top);
        }
        
        [MenuItem ("GameObject/Pivot/Left", false, PivotMenuPriority)]
        private static void SetPivotToLeft (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Left);
        }
        
        [MenuItem ("GameObject/Pivot/Right", false, PivotMenuPriority)]
        private static void SetPivotToRight (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Right);
        }
        
        [MenuItem ("GameObject/Pivot/Back", false, PivotMenuPriority)]
        private static void SetPivotToBack (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Back);
        }
        
        [MenuItem ("GameObject/Pivot/Front", false, PivotMenuPriority)]
        private static void SetPivotToFront (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Front);
        }

        private static void SetPivot (MenuCommand command, PivotPosition pivot)
        {
            var gameObject = command.context as GameObject;
            if (gameObject == null)
                return;

            var childMeshFilters = gameObject.GetComponentsInChildren<MeshFilter> ();
            var offset = GetPivotOffset (childMeshFilters, pivot);

            OffsetChildren (gameObject.transform, offset);
        }

        private static Vector3 GetPivotOffset (MeshFilter[] meshFilters, PivotPosition pivotPosition)
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


        private static void OffsetChildren (Transform transform, in Vector3 offset)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild (i).localPosition += offset;
            }
        }
    }
}