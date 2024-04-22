using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject closeStateOb;
    [SerializeField] private GameObject openStateOb;
    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;

    private void Start()
    {
        Player.Instance.OnWin += Player_OnWin;
        //GameManager.Instance.OnLoadLevel += GameManager_OnLoadLevel;

        Init();
    }

    private void OnDestroy()
    {
        Player.Instance.OnWin -= Player_OnWin;
    }

    /*
    private void GameManager_OnLoadLevel(object sender, System.EventArgs e)
    {
        Init();
    }
    */

    private void Player_OnWin(object sender, System.EventArgs e)
    {
        WinState();
    }

    private void Init()
    {
        closeStateOb.SetActive(true); 
        openStateOb.SetActive(false);
        particle1.SetActive(false);
        particle2.SetActive(false);
    }

    private void WinState()
    {
        closeStateOb.SetActive(false);
        openStateOb.SetActive(true);
        particle1.SetActive(true);
        particle2.SetActive(true);
    }
}
