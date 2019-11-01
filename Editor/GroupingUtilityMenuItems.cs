using UnityEditor;

namespace Hirame.Mercury.Editor
{
    
    public static class GroupingUtilityMenuItems
    {
        private const int GroupingToolPriority = -900;
        private const string MenuRootName = "GameObject";

        private static uint lastFrame = 0;

        [MenuItem (MenuRootName + "/Group under new", false, GroupingToolPriority)]
        private static void MenuItem (MenuCommand command)
        {
            if (!EditorUtility.IsNewFrame (ref lastFrame))
            {
                return;
            }
            
            var transforms = Selection.transforms;
            var newParent = TransformUtility.CreateGroup (transforms, true);
            Selection.activeTransform = newParent;
        }
    }

}
