using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static event EventHandler OnMenuButtoPress;

    [SerializeField] private Button continueBtn;
    [SerializeField] private Button playAgainBtn;
    [SerializeField] private Button homeBtn;

    private void Start()
    {
        GameManager.Instance.OnPauseGame += GameManager_OnPauseGame;

        continueBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.TooglePaused();
            Hide();
        });

        playAgainBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.TooglePaused();
            //Player.Instance.ClearBrick();
            GameManager.Instance.LoadLevel();
            //Debug.Log("Hide");
            Hide();
        });

        homeBtn.onClick.AddListener(() =>
        {
            OnMenuButtoPress?.Invoke(this, EventArgs.Empty);
            Hide();
        });

        Hide();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPauseGame -= GameManager_OnPauseGame;
    }

    private void GameManager_OnPauseGame(object sender, System.EventArgs e)
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
