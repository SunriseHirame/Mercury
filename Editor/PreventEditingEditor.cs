using System;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace Hirame.Mercury.Editor
{
    [CustomEditor (typeof (PreventEditing))]
    public class PreventEditingEditor : UnityEditor.Editor
    {
        private void OnEnable ()
        {
            CheckLocking ();
        }

        private void OnDisable ()
        {
            UnlockComponents ();
        }

        public override void OnInspectorGUI ()
        {
            EditorGUILayout.HelpBox ("This does not prevent values being altered by code!", MessageType.Info);

            using (var changeScope = new EditorGUI.ChangeCheckScope ())
            {
                DrawPropertiesExcluding (serializedObject, "m_Script");

                if (changeScope.changed)
                {
                    serializedObject.ApplyModifiedProperties ();
                    CheckLocking ();
                }
            }
        }

        private void CheckLocking ()
        {
            var lockEditing = target as PreventEditing;
            if (lockEditing == null)
                return;

            switch (lockEditing.LockBehavior)
            {
                case PreventEditing.LockType.None:
                    UnlockComponents ();
                    break;
                case PreventEditing.LockType.Override:
                    if (!IsRootPrefab (lockEditing))
                    {
                        LockComponents ();
                    }
                    else
                    {
                        UnlockComponents ();
                    }                    break;
                case PreventEditing.LockType.Prefab:
                    if (PrefabStageUtility.GetCurrentPrefabStage () != null
                        && PrefabUtility.IsAnyPrefabInstanceRoot (lockEditing.gameObject))
                    {
                        LockComponents ();
                    }
                    else
                    {
                        UnlockComponents ();
                    }
                    break;
                case PreventEditing.LockType.Total:
                    LockComponents ();
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }

        private bool IsRootPrefab (PreventEditing preventEditing)
        {
            if (PrefabStageUtility.GetCurrentPrefabStage () != null)
                return preventEditing.transform.parent == null;
                
            return false;
        }
        
        private void LockComponents ()
        {
            var lockEditing = target as PreventEditing;
            if (lockEditing == null)
                return;

            foreach (var component in lockEditing.GetComponents<Component> ())
            {
                component.hideFlags |= HideFlags.NotEditable;
            }

            if (lockEditing.LockBehavior == PreventEditing.LockType.Total)
                return;

            if (PrefabUtility.IsAnyPrefabInstanceRoot (lockEditing.gameObject))
            {
                foreach (var added in PrefabUtility.GetAddedComponents (lockEditing.gameObject))
                {
                    added.instanceComponent.hideFlags &= ~HideFlags.NotEditable;
                }
            }
        }

        private void UnlockComponents ()
        {
            var lockEditing = target as PreventEditing;
            if (lockEditing == null)
                return;

            foreach (var component in lockEditing.GetComponents<Component> ())
            {
                component.hideFlags &= ~HideFlags.NotEditable;
            }
        }

        [MenuItem ("CONTEXT/PreventEditing/Force Unlock")]
        private static void ForceUnlockLockComponent (MenuCommand command)
        {
            var hFolder = command.context as PreventEditing;
            if (hFolder != null)
                hFolder.hideFlags &= ~HideFlags.NotEditable;
        }
    }
}