using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelList;

    private void Awake()
    {
        //InitLevel();
    }


    private void Start()
    {
        GameManager.Instance.OnLoadLevel += GameManager_OnNextLevel;

        InitLevel();
        GetLevel(1);
    }

    private void GameManager_OnNextLevel(object sender, System.EventArgs e)
    {
        InitLevel();
        GetLevel(GameManager.Instance.GetCurrentLevel());
    }

    private void InitLevel()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].SetActive(false);
            //FileData.Instance.SaveData(levelList[i], i+1);
        }
    }

    private void GetLevel(int i)
    {
        //GameObject level = levelList[i-1];
        //Debug.Log(level);
        levelList[i-1].SetActive(true);
        //FileData.Instance.GetData(levelSpawner, i + 1);
    }
}
