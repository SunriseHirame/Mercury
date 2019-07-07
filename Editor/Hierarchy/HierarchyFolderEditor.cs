using UnityEditor;
using UnityEngine;

namespace Hirame.Mercury.Editor
{
    [CustomEditor (typeof (HierarchyFolder))]
    public sealed class HierarchyFolderEditor : UnityEditor.Editor
    {
        private Tool cachedTool;

        private static GUIStyle style;

        public override void OnInspectorGUI ()
        {
            Tools.hidden = true;

            if (style == null)
            {
                style = new GUIStyle(GUI.skin.box);
                style.richText = true;
                style.normal.textColor = Color.white;
            }

            GUILayout.Box (
                @"A component that is used to group objects in Editor.
This is object is <color=red>NOT</color> included in builds!", 
                style, GUILayout.ExpandWidth (true));
        }
    }

}