using UnityEditor;

namespace Hirame.Mercury.Editor
{
    [InitializeOnLoad]
    public static class EditorUtility
    {

        private static uint currentEditorFrame;

        public static uint CurrentEditorFrame => currentEditorFrame;
        
        static EditorUtility ()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        public static bool IsNewFrame (ref uint lastFrame)
        {
            var isNew = currentEditorFrame > lastFrame;
            lastFrame = currentEditorFrame;
            return isNew;
        }
        
        public static bool IsNewFrame (int lastFrame)
        {
            return currentEditorFrame > lastFrame;
        }
        
        private static void OnEditorUpdate ()
        {
            currentEditorFrame++;
        }

    }

}
