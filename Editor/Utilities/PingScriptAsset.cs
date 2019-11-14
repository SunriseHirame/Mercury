using UnityEditor;

namespace Hirame.Mercury.Editor
{


    public static class PingScriptAsset
    {
        [MenuItem ("CONTEXT/MonoBehaviour/Highlight Script Asset")]
        public static void MonoBehaviourSelectScriptAsset (MenuCommand command)
        {
            var fileName = $"{command.context.GetType ().Name}.cs";

            var scripts = AssetDatabase.FindAssets ("t:script");

            foreach (var t in scripts)
            {
                var path = AssetDatabase.GUIDToAssetPath (t);
                var startIndex = path.LastIndexOf ('/') + 1;
                var name = path.Substring (startIndex);

                if (!name.Equals (fileName))
                    continue;

                var obj = AssetDatabase.LoadAssetAtPath<MonoScript> (path);
                EditorGUIUtility.PingObject (obj);
                break;
            }
        }
    }
}