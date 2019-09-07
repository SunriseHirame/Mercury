using UnityEngine;

namespace Hirame.Mercury
{
    public sealed class PreventEditing : MonoBehaviour
    {
        public enum LockType
        {
            None,
            Override,
            Prefab,
            Total
        }

        public LockType LockBehavior = LockType.Override;
    }

}