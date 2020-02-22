using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hirame.Mercury
{
    [InitializeOnLoad]
public static class Snapshotter
{
    private const string PrefsSaveKey = "ResetData";
    
    static Snapshotter ()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged (PlayModeStateChange playModeStateChange)
    {
        switch (playModeStateChange)
        {
            case PlayModeStateChange.EnteredEditMode:
                RestoreSnapshots ();
                break;
            case PlayModeStateChange.ExitingEditMode:
                SaveSnapShots ();
                break;
        }
    }

    private static void SaveSnapShots ()
    {
        var saveData = new SaveData ();
        var guids = AssetDatabase.FindAssets ("t:scriptableObject");
        
        foreach (var guid in guids)
        {
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject> (AssetDatabase.GUIDToAssetPath (guid));
            if (asset is IResetOnPlayModeExit)
                saveData.Add (asset);
        }
        
        EditorPrefs.SetString (PrefsSaveKey, JsonUtility.ToJson (saveData));
    }

    private static void RestoreSnapshots ()
    {
        var rawSaveData = EditorPrefs.GetString (PrefsSaveKey, "");
        if (string.IsNullOrEmpty (rawSaveData))
            return;

        var saveData = JsonUtility.FromJson<SaveData> (rawSaveData);
        var guids = AssetDatabase.FindAssets ("t:scriptableObject");

        foreach (var guid in guids)
        {
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject> (AssetDatabase.GUIDToAssetPath (guid));
            if (asset is IResetOnPlayModeExit)
                saveData.Restore (asset);
        }
    }

    [Serializable]
    private sealed class SaveData : ISerializationCallbackReceiver
    {
        private readonly Dictionary<int, string> objectMap = new Dictionary<int, string> ();
        [SerializeField] private List<int> instanceIds = new List<int> ();
        [SerializeField] private List<string> jsonData = new List<string> ();
        
        internal void Add (Object obj)
        {
            Debug.Log (obj.name);
            instanceIds.Add (obj.GetInstanceID ());
            jsonData.Add (JsonUtility.ToJson (obj));
        }
        
        internal void Add<T> (IEnumerable<T> objs) where T : Object
        {
            foreach (var obj in objs)
            {
                Add (obj);
            }
        }

        internal void Restore<T> (T obj) where T : Object
        {
            if (objectMap.TryGetValue (obj.GetInstanceID (), out var json))
                JsonUtility.FromJsonOverwrite (json, obj);
            else
                Debug.LogError ($"Restore data not found for: {obj.name}");
        }

        internal void Restore<T> (IEnumerable<T> objs) where T : Object
        {
            foreach (var obj in objs)
            {
                Restore (obj);
            }
        }
        
        internal void Clear ()
        {
            objectMap?.Clear ();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize ()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize ()
        {
            for (var i = 0; i < instanceIds.Count; i++)
            {
                objectMap.Add (instanceIds[i], jsonData[i]);
            }
        }
    }
}

public interface IResetOnPlayModeExit { }

}