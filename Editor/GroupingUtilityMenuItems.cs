using UnityEditor;

namespace Hirame.Mercury.Editor
{
    
    public static class GroupingUtilityMenuItems
    {
        private const int GroupingToolPriority = -900;
        private const string MenuRootName = "GameObject/Tools";

        private static uint lastFrame = 0;

        [MenuItem (MenuRootName + "/Group/To New", false, GroupingToolPriority)]
        private static void GroupToNew (MenuCommand command)
        {
            if (!EditorUtility.IsNewFrame (ref lastFrame))
            {
                return;
            }
            
            var transforms = Selection.transforms;
            var newParent = TransformUtility.CreateGroup (transforms, GroupingMode.Center);
            Selection.activeTransform = newParent;
        }
        
        [MenuItem (MenuRootName + "/Group/To Folder", false, GroupingToolPriority)]
        private static void GroupToFolder (MenuCommand command)
        {
            if (!EditorUtility.IsNewFrame (ref lastFrame))
            {
                return;
            }
            
            var transforms = Selection.transforms;
            var newParent = TransformUtility.CreateGroup (transforms, GroupingMode.Folder);
            Selection.activeTransform = newParent;
        }
    }

}
