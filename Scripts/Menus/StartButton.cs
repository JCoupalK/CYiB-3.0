using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

     public void StartGame ()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
