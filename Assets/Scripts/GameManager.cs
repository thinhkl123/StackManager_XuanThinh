using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnLoadLevel;

    public int LevelMax = 5;

    private int level;

    private void Awake()
    {
        level = 1;
        Instance = this;
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
}
