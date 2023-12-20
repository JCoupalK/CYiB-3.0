using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BackButton2 : MonoBehaviour
{
   
    public GameObject SettingMenu;
    public GameObject PauseMenu;
    public GameObject DefaultSelectedElement;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SettingMenu.SetActive(false);         
        }
    }
    public void Back2()
    {
        PauseMenu.SetActive(true);
        SettingMenu.SetActive(false);
        if (DefaultSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
        }
    }
}