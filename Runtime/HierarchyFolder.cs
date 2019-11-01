using UnityEngine;

namespace Hirame.Mercury
{
    [ExecuteAlways]
    public sealed class HierarchyFolder : MonoBehaviour
    {
        [SerializeField] private bool includeInBuilds = true;
        [SerializeField] private string customMessage;

        public bool IsIncludedInBuilds
        {
            get => includeInBuilds;
            internal set => includeInBuilds = value;
        }
        
#if UNITY_EDITOR
        private void Update ()
        {
            if (Application.isPlaying)
                return;
            
            if (!transform.hasChanged || TransformHasDefaultValues ())
                return;

            ResetTransform ();
        }
#endif

        private void OnValidate ()
        {
            if (!includeInBuilds && !gameObject.CompareTag ("EditorOnly"))
                gameObject.tag = "EditorOnly";
            else if (gameObject.CompareTag ("EditorOnly"))
                gameObject.tag = "Untagged";
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
            var ownTransform = transform;
            var children = new Transform[ownTransform.childCount];

            for (var i = 0; i < children.Length; i++)
            {
                children[i] = transform.GetChild (0);
            }

            ownTransform.DetachChildren ();
            ownTransform.localPosition = Vector3.zero;
            ownTransform.localRotation = Quaternion.identity;
            ownTransform.localScale = Vector3.one;

            foreach (var child in children)
            {
                child.SetParent (ownTransform);
            }
        }
    }
}