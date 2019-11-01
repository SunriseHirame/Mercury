using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hirame.Mercury.Editor
{
    [InitializeOnLoad]
    public static class HierarchyDrawer
    {
        private static Dictionary<int, bool> drawMap = new Dictionary<int, bool> ();
        
        static HierarchyDrawer ()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnDrawHierarchyItem;
        }

        private static void OnDrawHierarchyItem (int instanceId, Rect rect)
        {
            if (!drawMap.TryGetValue (instanceId, out var customDrawing))
            {
                var go = UnityEditor.EditorUtility.InstanceIDToObject (instanceId) as GameObject;
                customDrawing = go && go.GetComponent<HierarchyFolder> ();
                drawMap.Add (instanceId, customDrawing);
            }

            if (!customDrawing)
                return;

            ColorStack.PushBackgroundColor (Color.green);
            GUI.Box (rect, GUIContent.none);
            ColorStack.PopBackgroundColor ();
        }
    }
}

