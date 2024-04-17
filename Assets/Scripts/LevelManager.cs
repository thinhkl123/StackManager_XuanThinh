using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {  get; private set; }

    [SerializeField] private List<GameObject> levelList;
    [SerializeField] private Transform levelSpawnPos;

    private void Awake()
    {
        instance = this;
        //InitLevel();
    }


    private void Start()
    {
        GameManager.Instance.OnLoadLevel += GameManager_OnLoadLevel;

        InitLevel();
        GetLevel(1);
    }

    private void GameManager_OnLoadLevel(object sender, System.EventArgs e)
    {
        InitLevel();
        GetLevel(GameManager.Instance.GetCurrentLevel());
    }

    private void InitLevel()
    {
        /*
        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].SetActive(false);
            //FileData.Instance.SaveData(levelList[i], i+1);
        }
        */
        //Debug.Log(levelSpawnPos.childCount.ToString());
        for (int i = 0; i < levelSpawnPos.childCount; i++)
        {
            //Debug.Log("Destroy " + i);
            Destroy(levelSpawnPos.GetChild(i).gameObject);
        }
    }

    private void GetLevel(int i)
    {
        //GameObject level = levelList[i-1];
        //Debug.Log(level);
        //Main
        ///levelList[i-1].SetActive(true);
        //FileData.Instance.GetData(levelSpawner, i + 1);

        Instantiate(levelList[i - 1], levelSpawnPos);
    }
}
