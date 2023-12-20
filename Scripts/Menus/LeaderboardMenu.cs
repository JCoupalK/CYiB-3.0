using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardMenu : MonoBehaviour
{
	public GameObject leaderboardMenu;
	public GameObject Menus;
	public GameObject DefaultSelectedElement;

	public void Start()
	{
		Cursor.visible = true;
		Menus.SetActive(true);
		leaderboardMenu.SetActive(false);
	}

	public void LeaderboardMenuOpen()
	{
		Menus.SetActive(false);
		leaderboardMenu.SetActive(true);
		if (DefaultSelectedElement != null)
		{
			EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
		}
	}
}
