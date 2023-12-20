using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CreditsButton : MonoBehaviour
{
	public GameObject CreditsMenu;
	public GameObject Menus;
	public GameObject DefaultSelectedElement;

	public void Start()
	{
		Cursor.visible = true;
		Menus.SetActive(true);
		CreditsMenu.SetActive(false);
	}

	public void CreditsMenuOpen()
	{
		Menus.SetActive(false);
		CreditsMenu.SetActive(true);
		if (DefaultSelectedElement != null)
		{
			EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
		}
	}


}
