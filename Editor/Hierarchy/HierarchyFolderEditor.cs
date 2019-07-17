using UnityEditor;
using UnityEngine;

namespace Hirame.Mercury.Editor
{
    [CustomEditor (typeof (HierarchyFolder))]
    public sealed class HierarchyFolderEditor : UnityEditor.Editor
    {
        private const string notIncludedInBuildsMessage = @"A component that is used to group objects in Editor.
This is object is <color=red>NOT</color> included in builds!";
        
        private static Tool cachedTool;
        private static HierarchyFolder folder;
        private static bool isEditingMessage;
            
        private static GUIStyle style;

        private void OnEnable ()
        {
            folder = target as HierarchyFolder;
            if (folder == null)
                return;
            
            folder.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
            folder.hideFlags = HideFlags.None;
        }

        public override void OnInspectorGUI ()
        {
            Tools.hidden = true;

            if (style == null)
            {
                style = new GUIStyle(GUI.skin.box);
                style.richText = true;
                style.normal.textColor = Color.white;
            }
            
            GUILayout.Box (notIncludedInBuildsMessage, style, GUILayout.ExpandWidth (true));

            var customMessageProp = serializedObject.FindProperty ("customMessage");

            using (var scope = new EditorGUI.ChangeCheckScope ())
            {
                if (!serializedObject.isEditingMultipleObjects && isEditingMessage)
                {
                    customMessageProp.stringValue = EditorGUILayout.TextArea (customMessageProp.stringValue, GUILayout.MinHeight (48));

                    if (GUILayout.Button ("Done"))
                    {
                        isEditingMessage = false;
                    }
                }
                else if (!string.IsNullOrEmpty (customMessageProp.stringValue))
                {
                    GUILayout.Box (customMessageProp.stringValue, style, GUILayout.ExpandWidth (true));
                }

                if (scope.changed)
                    serializedObject.ApplyModifiedProperties ();
            }
        }

        private void OnDisable ()
        {
            if (folder == null)
                return;
            
            folder.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
            folder.hideFlags = HideFlags.None;
            isEditingMessage = false;
        }
        
        [MenuItem ("CONTEXT/HierarchyFolder/Edit Message")]
        private static void EditCustomMessage (MenuCommand command)
        {
            var hFolder = command.context as HierarchyFolder;
            isEditingMessage = hFolder;
        }
    }

}