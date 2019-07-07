using UnityEngine;

namespace Hirame.Mercury
{
    [ExecuteAlways]
    public sealed class HierarchyFolder : MonoBehaviour
    {
        private void Update ()
        {
            if (!transform.hasChanged || TransformHasDefaultValues ())
                return;
            
            ResetTransform ();
        }

        private void OnValidate ()
        {
            gameObject.hideFlags = HideFlags.NotEditable;
            transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
            
            hideFlags = HideFlags.None;
            if (!gameObject.CompareTag ("EditorOnly"))
                gameObject.tag = "EditorOnly";
        }

        private bool TransformHasDefaultValues ()
        {
            var t = transform;
            return t.localPosition == Vector3.zero
                   && t.localRotation == Quaternion.identity
                   && t.localScale == Vector3.one;
        }

        private void ResetTransform ()
        {
            var t = transform;
            var children = new Transform[t.childCount];

            for (var i = 0; i < children.Length; i++)
            {
                children[i] = transform.GetChild (0);
            }
            
            t.DetachChildren ();
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.zero;

            foreach (var child in children)
            {
                child.SetParent (t);   
            }
        }
    }

}