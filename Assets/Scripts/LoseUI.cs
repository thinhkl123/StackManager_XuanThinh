using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Button playAgianBtn;

    private void Start()
    {
        Player.Instance.OnLose += Player_OnLose;

        playAgianBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadLevel();
            Hide();
        });

        Hide();
    }

    private void OnDestroy()
    {
        Player.Instance.OnLose -= Player_OnLose;
    }

    private void Player_OnLose(object sender, System.EventArgs e)
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
