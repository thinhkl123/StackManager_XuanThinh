using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button pauseBtn;

    private void Start()
    {
        GameManager.Instance.OnLoadLevel += GameManager_OnLoadLevel;

        pauseBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.TooglePaused();
        });
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLoadLevel -= GameManager_OnLoadLevel;
    }

    private void GameManager_OnLoadLevel(object sender, System.EventArgs e)
    {
        levelText.text = "Level " + GameManager.Instance.GetCurrentLevel().ToString();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
