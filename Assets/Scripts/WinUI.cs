using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    //[SerializeField] private Button playAgainButton;

    private void Start()
    {
        Player.Instance.OnWinLevel += Player_OnWinLevel;
        GameManager.Instance.OnLoadLevel += GameManager_OnNextLevel;

        nextLevelButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UpdateLevel(1);
            if (GameManager.Instance.GetCurrentLevel() <= GameManager.Instance.LevelMax)
            {
                GameManager.Instance.LoadLevel();
            }
        });

        /*
        playAgainButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UpdateLevel(0);
            GameManager.Instance.LoadLevel();
        });
        */

        Hide();
    }

    private void GameManager_OnNextLevel(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Player_OnWinLevel(object sender, System.EventArgs e)
    {
        Show();
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
