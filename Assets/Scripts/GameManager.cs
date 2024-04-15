using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnNextLevel;

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

    public void NextLevel()
    {
        level++;
        OnNextLevel?.Invoke(this, EventArgs.Empty); 
    }
}
