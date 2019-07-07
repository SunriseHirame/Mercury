using UnityEditor;
using UnityEngine;
using PivotPosition = Hirame.Mercury.HierarchyUtility.PivotPosition;

namespace Hirame.Mercury.Editor
{
    public class HierarchyUtilityMenuItems : MonoBehaviour
    {
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
            var offset = HierarchyUtility.GetPivotOffset (childMeshFilters, pivot);

            HierarchyUtility.OffsetChildren (gameObject.transform, offset);
        }
    }
}