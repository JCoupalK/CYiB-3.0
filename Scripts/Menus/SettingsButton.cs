using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour
{
    public GameObject SettingMenu;
    public GameObject Menus;
    public GameObject DefaultSelectedElement;

    public void Start()
    {
        Cursor.visible = true;
        Menus.SetActive(true);
        SettingMenu.SetActive(false);
    }

    public void SettingsMenuOpen()
    {
        SettingMenu.SetActive(true);
        Menus.SetActive(false);

        if (DefaultSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
        }
    }
}
