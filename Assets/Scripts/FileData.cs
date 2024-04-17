using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FileData : MonoBehaviour
{
    public static FileData Instance {  get; private set; }

    private string path;
    private Transform level;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveData(GameObject levelGo, int levelId)
    {
        path = $"Assets/FileData/Level_{levelId}.txt";

        string dataPrefab = EditorJsonUtility.ToJson(levelGo);

        File.WriteAllText(path, dataPrefab);
    }

    public void GetData(GameObject levelGO, int levelId)
    {
        if (levelGO == null)
        {
            Debug.Log("Error GO");
            return;
        }

        path = $"Assets/FileData/Level_{levelId}.txt";

        if (File.Exists(path))
        {
            string dataPrefab = File.ReadAllText(path);

            //EditorJsonUtility.FromJsonOverwrite(dataPrefab, level);

            EditorJsonUtility.FromJsonOverwrite(dataPrefab, levelGO.transform);

            //levelGO = level.gameObject;


            //Debug.Log(level.gameObject.name);
            GameObject levelPrefab = Instantiate(levelGO);

            Debug.Log("Succes");
          
        }
        else
        {
            Debug.LogError("File Null");
        }
    }
}
