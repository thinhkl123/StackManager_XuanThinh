using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] GameObject lockIcon;

    public void HideIcon()
    {
        lockIcon.SetActive(false);
    }    

    public void ShowIcon()
    {
        lockIcon.SetActive(true);
    }
}
