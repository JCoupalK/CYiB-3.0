using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackStoryButton : MonoBehaviour
{
    public GameObject BackStoryMenu;
    public GameObject VideoPlayer;
    public GameObject Menus;
    public GameObject MainCam;
    public GameObject BackStoryCam;
    public GameObject DefaultSelectedElement;

    public void Start()
    {
        BackStoryMenu.SetActive(false);
        VideoPlayer.SetActive(false);
        BackStoryCam.SetActive(false);
        MainCam.SetActive(true);
        Menus.SetActive(true);
    }

    
    public void BackStory()
    {
        BackStoryMenu.SetActive(true);
        VideoPlayer.SetActive(true);
        BackStoryCam.SetActive(true);
        MainCam.SetActive(false);
        Menus.SetActive(false);
        if (DefaultSelectedElement != null)
        {
            EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
        }
    }
}
