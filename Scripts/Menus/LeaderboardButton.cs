using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LeastSquares;
public class LeaderboardButton : MonoBehaviour
{
	public GameObject LeaderboardMenu;
	public GameObject Menus;
	public GameObject DefaultSelectedElement;
	public LeaderboardUI leaderboardUI;

	public void Start()
	{
		Cursor.visible = true;
		Menus.SetActive(true);
		LeaderboardMenu.SetActive(false);
	}

	public void LeaderboardMenuOpen()
	{
		Menus.SetActive(false);
		LeaderboardMenu.SetActive(true);
		leaderboardUI.RefreshScores();
		if (DefaultSelectedElement != null)
		{
			EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
		}
	}
}
