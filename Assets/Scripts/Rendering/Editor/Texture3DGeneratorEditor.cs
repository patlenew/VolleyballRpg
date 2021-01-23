using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Texture3DGenerator))]
public class Texture3DGeneratorEditor : Editor
{
    public string textureName = "Cloud";
    public const string path = "Assets/Art/Generated/";
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Texture3DGenerator generator = (Texture3DGenerator)target;

        textureName = EditorGUILayout.TextField("Filename", textureName);
        if (GUILayout.Button("Generate Texture 2D"))
        {
            SaveTextureAsPNG(generator.GenerateTexture2D());
        }
        if (GUILayout.Button("Generate Texture 3D"))
        {
            SaveTextureAsAsset(generator.GenerateTexture3D());
        }
    }

    public void SaveTextureAsPNG(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToJPG();
        System.IO.File.WriteAllBytes(path + textureName + ".jpg", bytes);
        Debug.Log(textureName + " is saved");
        AssetDatabase.Refresh();
    }

    public void SaveTextureAsAsset(Texture3D texture)
    {
        AssetDatabase.CreateAsset(texture, path + textureName + ".asset");
        Debug.Log(textureName + " is saved");
        AssetDatabase.Refresh();
    }
}
