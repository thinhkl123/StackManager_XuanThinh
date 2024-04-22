using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    public static event EventHandler OnBackButtonPress;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject LevelList;
    [SerializeField] private Button backBtn;

    private List<GameObject> buttonLevelList;

    private void Awake()
    {
        buttonLevelList = new List<GameObject>();
    }

    private void Start()
    {
        HomeUI.OnPlayButtonPress += HomeUI_OnPlayButtonPress;
        GameManager.Instance.OnUpdateLevel += GameManager_OnUpdateLevel;
        PauseUI.OnMenuButtoPress += PauseUI_OnMenuButtoPress;

        backBtn.onClick.AddListener(() =>
        {
            OnBackButtonPress?.Invoke(this, EventArgs.Empty);
            Hide();
        });

        InitButton();

        Hide();
    }

    private void PauseUI_OnMenuButtoPress(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnUpdateLevel(object sender, System.EventArgs e)
    {
        UnLockLevel(GameManager.Instance.GetCurrentLevel());
    }

    private void OnDestroy()
    {
        HomeUI.OnPlayButtonPress -= HomeUI_OnPlayButtonPress;
        GameManager.Instance.OnUpdateLevel -= GameManager_OnUpdateLevel;
        PauseUI.OnMenuButtoPress -= PauseUI_OnMenuButtoPress;
    }

    private void HomeUI_OnPlayButtonPress(object sender, System.EventArgs e)
    {
        Show();
    }

    private void InitButton()
    {
        int curLevel = GameManager.Instance.GetCurrentLevel();

        //Debug.Log(curLevel);

        for (int i = 0; i<GameManager.Instance.LevelMax; i++)
        {
            GameObject buttonLevelGO = Instantiate(buttonPrefab, LevelList.transform);
            buttonLevelGO.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
            //Debug.Log(i+1);
            buttonLevelGO.SetActive(true);
            buttonLevelList.Add(buttonLevelGO);
            if (i+1 <= curLevel)
            {
                buttonLevelGO.GetComponent<ButtonLevel>().HideIcon();
                buttonLevelGO.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int level = int.Parse(buttonLevelGO.GetComponentInChildren<TextMeshProUGUI>().text);
                    //Debug.Log(level);
                    GameManager.Instance.SetLevel(level);
                    GameManager.Instance.LoadLevel();
                    GameManager.Instance.TooglePaused();
                    Hide();
                });
            }
            else
            {
                buttonLevelGO.GetComponent<ButtonLevel>().ShowIcon();
            }
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }    

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void UnLockLevel(int level)
    {
        GameObject buttonLevelGO = buttonLevelList[level-1];
        buttonLevelGO.GetComponent<ButtonLevel>().HideIcon();
        buttonLevelGO.GetComponent<Button>().onClick.AddListener(() =>
        {
            int level = int.Parse(buttonLevelGO.GetComponentInChildren<TextMeshProUGUI>().text);
            Debug.Log(level);
            GameManager.Instance.SetLevel(level);
            GameManager.Instance.LoadLevel();
            GameManager.Instance.TooglePaused();
            Hide();
        });
    }
}
