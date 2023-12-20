using UnityEngine;
using Steamworks;

public class QuitButton : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("QUIT");
        SteamClient.Shutdown();
        Application.Quit();
    }


}
