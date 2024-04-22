using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public static event EventHandler OnPlayButtonPress;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        SelectLevelUI.OnBackButtonPress += SelectLevelUI_OnBackButtonPress;

        playButton.onClick.AddListener(() =>
        {
            //GameManager.Instance.TooglePaused();
            OnPlayButtonPress?.Invoke(this, EventArgs.Empty);   
            Hide();
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void OnDestroy()
    {
        SelectLevelUI.OnBackButtonPress -= SelectLevelUI_OnBackButtonPress;
    }

    private void SelectLevelUI_OnBackButtonPress(object sender, EventArgs e)
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
