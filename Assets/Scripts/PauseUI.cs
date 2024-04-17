using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button playAgainBtn;

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
            Player.Instance.ClearBrick();
            GameManager.Instance.LoadLevel();
            //Debug.Log("Hide");
            Hide();
        });

        Hide();
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
