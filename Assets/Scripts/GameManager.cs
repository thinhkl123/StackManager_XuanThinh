using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnLoadLevel;
    public event EventHandler OnPauseGame;

    public int LevelMax = 5;

    private int level;
    private bool isPaused;

    private void Awake()
    {
        level = 1;
        Instance = this;
        isPaused = true;
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    public void UpdateLevel(int bonus)
    {
        level += bonus;
    }

    public void LoadLevel()
    {
        //level++;
        OnLoadLevel?.Invoke(this, EventArgs.Empty); 
    }

    public void TooglePaused()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            OnPauseGame?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
        }

        Debug.Log(isPaused);
    }
}
