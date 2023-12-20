using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinsButton : MonoBehaviour
{
	public GameObject SkinsMenu;
	public GameObject Menus;
	public GameObject DefaultSelectedElement;

	public void Start()
	{
		Cursor.visible = true;
		Menus.SetActive(true);
		SkinsMenu.SetActive(false);
	}

	public void SkinsMenuOpen()
	{
		Menus.SetActive(false);
		SkinsMenu.SetActive(true);
		if (DefaultSelectedElement != null)
		{
			EventSystem.current.SetSelectedGameObject(DefaultSelectedElement);
		}
	}


}



