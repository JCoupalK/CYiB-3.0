using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GroundSpawner groundSpawner;


    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GetComponent<GroundSpawner>();
    }


    public void SpawnTriggerEntered()
    {
        groundSpawner.moveGround();
    }
}
