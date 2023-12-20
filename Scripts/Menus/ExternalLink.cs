using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalLink : MonoBehaviour
{
	public void OpenKorbTwitter()
	{
		Application.OpenURL("https://twitter.com/KorbCup");
	}

	public void OpenNecroTwitter()
	{
		Application.OpenURL("https://twitter.com/necrocatic");
	}
}
