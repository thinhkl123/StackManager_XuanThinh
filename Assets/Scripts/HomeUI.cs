using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TooglePaused();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
