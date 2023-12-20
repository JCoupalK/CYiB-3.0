using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour
{
    public GameObject BackStoryMenu;
    public GameObject VideoPlayer;
    public GameObject Menus;
    public GameObject SettingMenu;
    public GameObject SkinsMenu;
    public GameObject CreditsMenu;
    public GameObject LeaderboardMenu;
    public GameObject MainCam;
    public GameObject BackStoryCam;
    public GameObject DefaultSelectedElement;

    public void Back()
    {
        // Disable every other menu
        SettingMenu.SetActive(false);
        BackStoryMenu.SetActive(false);
        VideoPlayer.SetActive(false);
        BackStoryMenu.SetActive(false);
        BackStoryCam.SetActive(false);
        SkinsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        LeaderboardMenu.SetActive(false);


        // Main Menu
        Menus.SetActive(true);
        MainCam.SetActive(true);
        if (DefaultSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
        }
    }
}
