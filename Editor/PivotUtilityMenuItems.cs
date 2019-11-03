using UnityEditor;
using UnityEngine;
using PivotPosition = Hirame.Mercury.HierarchyUtility.PivotPosition;

namespace Hirame.Mercury.Editor
{
    public class PivotUtilityMenuItems : MonoBehaviour
    {
        private const int PivotMenuPriority = -900;
        private const string MenuRootName = "GameObject/Tools/Pivot";


        [MenuItem (MenuRootName + "/Center", false, PivotMenuPriority)]
        private static void SetPivotToCenter (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Center);
        }

        [MenuItem (MenuRootName + "/Bottom", false, PivotMenuPriority)]
        private static void SetPivotToBottom (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Bottom);
        }

        [MenuItem (MenuRootName + "/Top", false, PivotMenuPriority)]
        private static void SetPivotToTop (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Top);
        }

        [MenuItem (MenuRootName + "/Left", false, PivotMenuPriority)]
        private static void SetPivotToLeft (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Left);
        }

        [MenuItem (MenuRootName + "/Right", false, PivotMenuPriority)]
        private static void SetPivotToRight (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Right);
        }

        [MenuItem (MenuRootName + "/Back", false, PivotMenuPriority)]
        private static void SetPivotToBack (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Back);
        }

        [MenuItem (MenuRootName + "/Front", false, PivotMenuPriority)]
        private static void SetPivotToFront (MenuCommand command)
        {
            SetPivot (command, PivotPosition.Front);
        }

        private static void SetPivot (MenuCommand command, PivotPosition pivot)
        {
            var gameObject = command.context as GameObject;
            if (gameObject == null)
                return;

            Undo.RegisterFullObjectHierarchyUndo (gameObject, "Set Pivot");

            var childMeshFilters = gameObject.GetComponentsInChildren<MeshFilter> ();
            var offset = HierarchyUtility.GetPivotOffset (childMeshFilters, pivot);

            HierarchyUtility.OffsetChildren (gameObject.transform, offset);
        }
    }
}