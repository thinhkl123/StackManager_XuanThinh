using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnLoadLevel;
    public event EventHandler OnPauseGame;
    public event EventHandler OnUpdateLevel;

    public int LevelMax;

    private int level;
    private bool isPaused;

    private void Awake()
    {
        //PlayerPrefs.SetInt("Level", 1);
        level = PlayerPrefs.GetInt("Level",1);
        Instance = this;
        isPaused = true;
        Time.timeScale = 0f;
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    public void UpdateLevel(int bonus)
    {
        level += bonus;
        PlayerPrefs.SetInt("Level", level);
        OnUpdateLevel?.Invoke(this, EventArgs.Empty);
    }

    public void SetLevel(int idx)
    {
        level = idx;
        //PlayerPrefs.SetInt("Level", level);
        int curLev = PlayerPrefs.GetInt("Level");
        if (level > curLev)
        {
            PlayerPrefs.SetInt("Level", curLev);
        }
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

        //Debug.Log(isPaused);
    }
}
