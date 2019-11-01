using System;
using UnityEngine;

namespace Hirame.Mercury
{
    public static class HierarchyUtility
    {
        public enum PivotPosition : int
        {
            Center = 0,
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4,
            Front = 5,
            Back = 6
        }

        private delegate void PivotDel (ref Vector3 offset, Vector3 extents);
        
        private static PivotDel[] offsetActions =
        {
            // Center
            (ref Vector3 offset, Vector3 extents) => 
            { },
            // Top
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.y > -extents.y)
                    offset.y = -extents.y;
            },
            // Bottom
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.y < extents.y)
                    offset.y = extents.y;
            },
            // Left
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.x < extents.x)
                    offset.x = extents.x;
            },
            // Right
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.x > -extents.x)
                    offset.x = -extents.x;
            },
            // Front
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.z > -extents.z)
                    offset.z = -extents.z;
            },
            // Back
            (ref Vector3 offset, Vector3 extents) =>
            {
                if (offset.z < extents.z)
                    offset.z = extents.z;
            },
        };
        
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
                    offsetActions[(int) pivotPosition] (ref offset, bounds.extents);
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
        
    }
}