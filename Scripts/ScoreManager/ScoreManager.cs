using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
	public void Update()
    {
        if (scoreText.enabled == true)
        {
            scoreText.text = player.position.z.ToString("0");
        }
    }
}

