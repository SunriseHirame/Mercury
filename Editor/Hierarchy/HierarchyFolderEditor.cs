using UnityEditor;

namespace Hirame.Mercury.Editor
{
    [CustomEditor (typeof (HierarchyFolder))]
    public sealed class HierarchyFolderEditor : UnityEditor.Editor
    {
        private Tool cachedTool;
        
        public override void OnInspectorGUI ()
        {
            Tools.hidden = true;
            //DrawPropertiesExcluding (serializedObject, "m_Script");
        }
    }

}