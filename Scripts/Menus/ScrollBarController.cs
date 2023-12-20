using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float sensitivity = 1.0f;

    void Update()
    {
        float input = Input.GetAxis("Vertical") * sensitivity;
        scrollbar.value += input * Time.deltaTime;
    }
}