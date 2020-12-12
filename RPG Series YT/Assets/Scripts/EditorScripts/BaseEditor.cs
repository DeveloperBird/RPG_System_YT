using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class BaseEditor : EditorWindow {

    private ScriptableObject assetData;

    public string assetName;

    private string GetDataType()
    {
        var dataType = GetType().ToString().Replace("Editor", "");
        return dataType;
    }

    public void DrawGUI<T>() where T : ScriptableObject
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(GetDataType() + " Creator!", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        assetName = EditorGUILayout.TextField("Name", assetName);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (assetData == null)
        {
            assetData = CreateInstance<T>();
        }

        var editor = Editor.CreateEditor(assetData);

        if (editor != null)
        {
            editor.OnInspectorGUI();
        }

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create " + GetDataType(), GUILayout.MaxWidth(200)))
        {
            ScriptableObject asset = CreateAsset<T>();

            SetStats(assetData, asset);
   
            assetData = CreateInstance<T>();

            EditorGUIUtility.PingObject(asset);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private ScriptableObject CreateAsset<T>() where T : ScriptableObject
    {
        T asset = CreateInstance<T>();

        string path = "Assets/ScriptableObjects/" + GetDataType() + "/";

        string assetPath = AssetDatabase.GenerateUniqueAssetPath(CheckPath(path) + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPath);

        return asset;
    }

    public virtual void SetStats(ScriptableObject assetData, ScriptableObject asset)
    {

    }

    private string CheckPath(string path)
    {
        if (!AssetDatabase.IsValidFolder(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

}
